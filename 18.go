package main

import (
	"bufio"
	"log"
	"os"
	"strconv"
	"strings"
	"sync"
	"time"
)

const queueSize = 1000

type Registers map[string]int

func (r Registers) Get(register string) int {
	v, ok := r[register]
	if !ok {
		r[register] = 0
		return r[register]
	}

	return v
}

func (r Registers) Set(register string, value int) {
	r[register] = value
}

type Program struct {
	ValuesSent int

	id      int
	sndChan chan<- int
	rcvChan <-chan int

	registers Registers
}

func NewProgram(id int, sndChan chan<- int, rcvChan <-chan int) *Program {
	registers := Registers{
		"p": id,
	}
	return &Program{
		id:        id,
		sndChan:   sndChan,
		rcvChan:   rcvChan,
		registers: registers,
	}
}

func (p *Program) getValue(s string) int {
	v, err := strconv.Atoi(s)
	if err != nil {
		v = p.registers.Get(s)
	}

	return v
}

func (p *Program) Run(instructions []string, wg *sync.WaitGroup) {
	defer wg.Done()
	index := 0

	for {
		instruction := instructions[index]
		parts := strings.Fields(instruction)

		switch parts[0] {
		case "snd":
			v := p.getValue(parts[1])
			p.ValuesSent++
			p.sndChan <- v
		case "set":
			v := p.getValue(parts[2])
			p.registers.Set(parts[1], v)
		case "add":
			v := p.getValue(parts[2])
			p.registers.Set(parts[1], p.registers.Get(parts[1])+v)
		case "mul":
			v := p.getValue(parts[2])
			p.registers.Set(parts[1], p.registers.Get(parts[1])*v)
		case "mod":
			v := p.getValue(parts[2])
			p.registers.Set(parts[1], p.registers.Get(parts[1])%v)
		case "rcv":
			var v int
			select {
			case v = <-p.rcvChan:
			case <-time.After(2 * time.Second):
				return
			}

			p.registers.Set(parts[1], v)
		case "jgz":
			v := p.getValue(parts[1])
			offset := p.getValue(parts[2])
			if v > 0 {
				index += offset
				continue
			}
		}

		index++
		if index < 0 || index >= len(instructions) {
			return
		}
	}
}

func main() {
	f, err := os.Open("18.txt")
	if err != nil {
		log.Fatal(err)
	}

	var instructions []string
	scanner := bufio.NewScanner(f)
	for scanner.Scan() {
		instructions = append(instructions, scanner.Text())
	}
	if scanner.Err() != nil {
		log.Fatal(scanner.Err())
	}

	wg := &sync.WaitGroup{}
	wg.Add(2)
	c1 := make(chan int, queueSize)
	c2 := make(chan int, queueSize)

	program0 := NewProgram(0, c1, c2)
	program1 := NewProgram(1, c2, c1)
	go program0.Run(instructions, wg)
	go program1.Run(instructions, wg)

	wg.Wait()
	log.Println(program1.ValuesSent)
}
