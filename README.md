# KnapsackProblem
Sample academic project for comparing solutions made by genetic and tabu search algorithms.

# Usage
## Input representation
Input is represented by JSON file in the following way (sample case)

```javascript
[
  {
    "Items": [
      {
        "Weight": 70,
        "Cost": 260
      },
      {
        "Weight": 60,
        "Cost": 245
      },
      {
        "Weight": 50,
        "Cost": 200
      },
      {
        "Weight": 40,
        "Cost": 100
      },
      {
        "Weight": 30,
        "Cost": 80
      },
      {
        "Weight": 20,
        "Cost": 65
      },
      {
        "Weight": 10,
        "Cost": 60
      },
      {
        "Weight": 10,
        "Cost": 60
      },
      {
        "Weight": 1,
        "Cost": 10
      }
    ],
    "KnapsackVolume": 5,
    "ItemsLength": 9
  }
]
```
## Running program
Main program arguments:
```
-w, --wait         (Default: False) If enabled, program waits for input to
                   terminate application.

-a, --algorithm    Required. Algorithm name used to solve knapsack problem.

-f, --file         Required. File with possible knapsack items configuration.

-s, --shortened    (Default: False) Shortened output (only cost, without items)
```

## Supported algorithm
### Genetic algorithm
Standard genetic algorithm (arguemnt name: **genetic**). When selected, additional parameters available:
```
-g, --generations      (Default: 1000) Number of generations.

-p, --population       (Default: 100) Number of chromosomes in 
                       one population.

-m, --mutation         (Default: 0.1) Probability of child
                       chromosome mutation.

--force-generations    (Default: False) Forces to use all generations to
                       search solution. Otherwise, when 75% has the same
                       fitness - algorithm is ended.
```

### Tabu search algorithm
Sample tabu search algorithm implementation (name: **tabu**) with additional arguments:
```
-t, --tabu              (Default: 150) Maximum size of tabu search list.

-i, --iterations        (Default: 3000) Number of iterations.

-n, --neighbourhoods    (Default: 200) Quantity of neighbourhoods generated
                        for best solution.

-s, --selection         (Default: swap) Selection algorithm name.
```
Currently, there are three selection algorthms to use: **copy**, **swap** and **shift**.

## Sample usage
```
C:\Knapsack\Bin>KnapsackProblem.exe --shortened -f tests/small.txt -a tabu -i 100 -n 100
```
Result
```
=====
Result of case 1: 0 in 0:00:00:00,0092364 ms
=====
Result of case 2: 0 in 0:00:00:00,0030354 ms
=====
Result of case 3: 200 in 0:00:00:00,0043843 ms
=====
Result of case 4: 345 in 0:00:00:00,0032112 ms
=====
Result of case 5: 1010 in 0:00:00:00,0033472 ms
=====
Result of case 6: 65536 in 0:00:00:00,0031518 ms
=====
Result of case 7: 0 in 0:00:00:00,0028661 ms
=====
Result of case 8: 64512 in 0:00:00:00,0046986 ms
=====
Result of case 9: 4096 in 0:00:00:00,0034032 ms
=====
Result of case 10: 8 in 0:00:00:00,0033318 ms
```
