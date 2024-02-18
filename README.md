# dLabCodingTest
Coding test that I got from dLab

Task was to convert from one custom format to a xml-based.
As it was pretty much zero requirements I setup my own goals for this one.

1. Should be a pluggable three component solution, consisting of inputs, transformation engine and outputs.
Inputs should transform into a intermediate format decided by the transformation engine and then the outputs should work with this IL.
The advantages are that when multiple outputs are in place, a new input can directly convert to all existing outputs.

2. The convertion should support streaming, meaning that the IL produced from the inputs are directly parsed to all connected outputs.
My ambition here was to actually let the outputs work completely independent from eachother but my initial design didn't support this so now the slowest output sets the pace :(
Would probably need some rework with the Memory and Span types to let the outputs itereate independently without copying memory.

3. Keep memory allocation low.
This is achieved with the streaming as only one "IL Node" is allocated at a time, could probably be even better with Spans here as well but they currently doesn't support yield(inside iterator)

4. Easy to unit test.
Think I got this quite well. Didn't do 100% code coverage but the examples are there.

5. Should be really easy to add another "Token Parser" for the custom format input.
Just create a RowParser following the used pattern, this is then automatically injected with an ioc based factory pattern.

# What I'm not so happy about
The way of closing dangling/open nodes isn't the most readable in the code. It's easy to maintain though.
Noticed that the example XML didn't have the same order for the tags as the custom format. Will see if I fix this in the output or let it slide.

# Improvements that could be made
The things mentioned above, but also Input validation rules. 
For example only the "P" tag should be allowed to be a root node. 
Add xsd validation support for the xml output.

One could argue that it lacks comments, and yes it probably does.

# How to run the program.
Just compile and set the CLI as startup project. It will look for any .txt files residing in the bin folder.
Then pick which one you want to parse and it will both use the Console Output & Xml Output(will create a [filename.xml]) 

# Total time spent is currently about 6-8 hours.
