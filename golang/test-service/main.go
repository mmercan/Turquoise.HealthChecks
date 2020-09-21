package main

import "fmt"

func main() {
	var i int
	i = 90
	firstname := "matt"

	c := complex(3, 4)
	r, u := real(c), imag(c)
	fmt.Println(i, firstname, r, u, "hello wolrd!")
}
