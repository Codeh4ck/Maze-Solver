# Maze Solver
A project demonstrating the solution of maze matrixes using the Breadth-first search algorithm.

# Project features
* Treats mazes as block matrixes (x, y coordinates - 2D integer array)
* Each block has a unique node number (in serial format, e.g (0, 0) = 0, (0, 1) = 1...)
* Node number is related to its coordinates (x = NodeNumber / MatrixColumns, y = NodeNumber - NodeNumber / MatrixColumns * MatrixColumns)
* Breadth-first algorithm determines the shortest path to the exit.
* Determines whether there is an exit or not.
* You can design your own maze (add walls) and click-drag to find the shortest path to your exit point.
* Can load example/pre-defined mazes from files (the maze blueprint file is in the form of numbers separated by commas)
* Numbers in the file indicate tile type (0 = Walkable Tile, 1 = Entrance, 2 = Exit, 3 = Wall)
* Visual presentation of the map, including walls, entrance & exit (if loaded from file) and the shortest exit path (if there is one)
* Supports mazes of up to Int32.MaxValue Row & Column size (note: you must have a huge monitor to display it though, hehe)
* Displays path block coordinates in text form for loaded mazes.
* Code is clean, commented on complex parts and has documentation comments. If you have questions, feel free to e-mail me (e-mail below)
* Friendly, open-minded coder that will listen to your questions / feedback or inquiries.

# Disclaimer
This project has been written fully by me, **Nikolas Andreou** (*Delirium*), a Greek C# programmer. The reason I wrote this example is for a programming challenge for the **Alliance for Digital Employability 1st Coding Bootcamp of Athens** and the **e-Travel** company. If you have any questions, feedback, suggestions, bug report, feel free to create an issue or even contact me at deliriumghs@hotmail.gr

# Screenshots

* **Manual maze builder and the solution path**

![Imgur](http://i.imgur.com/aE43nA9.png)


* **Maze loaded from file (Green marks the entrance, Red marks the exit)**

![Imgur](http://i.imgur.com/26jDwUl.png)


* **Maze loaded from file with the solution included (also in text form)**

![Imgur](http://i.imgur.com/RKnrAEa.png)


_**Happy maze solving and learning!**_
