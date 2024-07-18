## Why did I make this?
I started doing MQO runs on ORIWOTW a few months ago. I love that the Ori debug tool allows you to have a collection of saves (.uberstate files) so you can create dozens of practice points throughout your speedrun
and load them with the debug menu.

![](https://i.gyazo.com/2eaa396bf1070b2367a6da2fd465910d.png)
![](https://i.gyazo.com/33e77d9506311fe612dc69f3fd5b07d4.png)

***These save files appear to be ordered based on Windows Explorer's 'sort by name'. So they display the same way in-game as they do in Windows explorer list mode sorted by name. This is why in the file names, we add
numbers to index them to make them ordered how we want them to be (e.g., I order mine in the same order that I reach those save points in a speedrun)***

### The issue with this
As I get better at running and decide to try new tricks, I sometimes realize that I want to insert a save point between two other save points within my directory.
This is annoying to do, because we control the order of the saves by numbering them (due to the alphanumeric sorting of the debug tool). So if I wanted to add a save between `007 - Door Skip` 
and `008 - Grab Regenerate`, I would have to manually rename `008 - Grab Regenerate` to `009`, and then repeat this process on `009` and above (which can be dozens of saves). So far I've only been practicing MQO but I've
already ran into this problem multiple times. I could only imagine what it is like for people who have hundreds of saves and want to insert some new ones to try different tricks.

### My Solution
That's where this tool comes in. It allows you to select a save file, and then press upshift of downshift. For example if I selected `008 - Grab Regenerate` and pressed `shift upwards`, it will automatically shift the
number (`008`) up by one, as well as any files that come after it (so `009`, `010` ... `024`) (*based on Windows Explorer 'sort by name'*), which can save you a lot of work.

Shifting downwards is similar: it modifies the selected file itself as well as the files that come *after* it based on name sorting, the only difference is that the numbers will go down by one. This can be used if you 
decide you no longer need a save point. You can just delete the save point that you no longer want, then downshift the save point above it.

### Warning

I have not tested this too too much. I coded this in one day and I attempted to make it as robust as possible. It works beautifully on my saves (which are formatted the same was as shown above), but if yours are
formatted ***drastically*** different from mine, it may not work as expected. It simply uses a regular expression to find repeating sequences of numbers at the beginning of the file names.

#### Here's a few examples of ones that work:

![](https://i.gyazo.com/5a7dba22ecd9072ea913c81caa40f2cf.png)

Also, it does not matter if you have 3 digits, less, or more:<br>
![](https://i.gyazo.com/a647b1c4aaaaba2eaaef63fdddfcff84.png)
