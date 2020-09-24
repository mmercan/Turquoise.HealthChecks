package main

import "fmt"

func main() {
	//	var i int
	//	i = 90
	//	firstname := "matt"
	//	c := complex(3, 4)
	//	r, u := real(c), imag(c)
	//	fmt.Println(i, firstname, r, u, "hello wolrd!")

	//var firstname *string = new(string)
	//*firstname = "Matt"
	//fmt.Println(*firstname)
	const pi = 3.14

	firstname := "Matt"
	ptr := &firstname
	fmt.Println(*ptr)
	firstname = "Esra"
	fmt.Println(*ptr)
}
