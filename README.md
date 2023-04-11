# Production Line Simulation

This project is a simulation of a production line with a single conveyor belt and three pairs of workers. Components of type A and B come onto the start of the belt at random intervals. Workers must take one component of each type from the belt as they come past and combine them to make a finished product (P).

# Assumptions
- There will always be a pair of workers to each slot
- Workers can interact with the first slot when components are generated
- Workers can't pick another component until they have finished assembling the product

# Usage
This is a C#.Net solution built with Visual Studio 2022 and the .Net 6 framework. 

# Unit Testing
The project includes unit tests to validate the behavior of the 'ConveyorBelt' and 'Worker' classes. 
The tests make use of dependency injection to replace the random component generation with a fixed set of components. 
The tests are not complete due to time constraints. 
