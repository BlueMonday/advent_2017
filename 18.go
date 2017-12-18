package main

import (
	"bufio"
	"log"
	"os"
	"strconv"
	"strings"
	"sync"
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
	otherP  *Program

	receivingMutex sync.RWMutex
	receiving      bool

	registers Registers
	stop      chan struct{}
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
		stop:      make(chan struct{}),
	}
}

func (p *Program) getValue(s string) int {
	v, err := strconv.Atoi(s)
	if err != nil {
		v = p.registers.Get(s)
	}

	return v
}

func (p *Program) SetOtherProgram(otherP *Program) {
	p.otherP = otherP
}

func (p *Program) Receiving() bool {
	p.receivingMutex.RLock()
	b := p.receiving
	p.receivingMutex.RUnlock()
	return b
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
			if len(p.rcvChan) == 0 && len(p.sndChan) == 0 && p.otherP.Receiving() {
				p.otherP.Stop()
				return
			}

			p.receivingMutex.Lock()
			p.receiving = true
			p.receivingMutex.Unlock()

			var v int
			select {
			case v = <-p.rcvChan:
			case <-p.stop:
				return
			}

			p.receivingMutex.Lock()
			p.receiving = false
			p.receivingMutex.Unlock()

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

func (p *Program) Stop() {
	close(p.stop)
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
	program0.SetOtherProgram(program1)
	program1.SetOtherProgram(program0)
	go program0.Run(instructions, wg)
	go program1.Run(instructions, wg)

	wg.Wait()
	log.Println(program1.ValuesSent)
}
