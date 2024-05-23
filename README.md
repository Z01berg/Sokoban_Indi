# Sokoban_Indi

# Control

## 1
WASD - Move
2 - Undo
3 - Redo
Zoom in/out - Mouse Scroll
P - Skip Level

## 2
←↑→↓ - Move
Z - Undo
X - Redo
Zoom in/out - Mouse Scroll
P - Skip Level

# Level Creator
Inside Assets/Resources/Data/Levels.txt jest txt który zawiera levely do rozegrania

## Rules

```
$ - Name of level
/ - End of level

- - empty
* - floor
# - wall

@ - player

5 - Red box 
% - Red box target

6 - Blue box
^ - Blue box target

7 - Green box
& - Green box target
```

## Example
```
$ Example Name
-----------########
-----------#******#
############******#
#****#************#
#****#5****#*****%#
#**********#*****^#
#****#6****########
#*@**#*****#-------
############-------
/
```