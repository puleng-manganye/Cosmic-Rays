// variables
VAR money = 0


=== Granny ===
Trick or Treat! #Me 

I have no tricks but I have some Candy Corn #Granny
Would you like some? #Granny

...#Me

* Yes please -> choice_301
+ No thanks -> choice_302

=== choice_301 === 
// Yes please.

Candy corn is my favourite candy :D #Me

Here you go #Granny
~money += 8 // Give 8 Candy Corn

Eight candy corn #Granny
 
Thanks #Me
My candy bag was empty but thanks to you, I got some sweet candy corn #Me

-> Amandla_1

=== choice_302 ===
// No thanks.

... #Me

Okay #Granny

-> Amandla_1

=== Amandla_1 ===
I woke up late and missed Trick or Treating #Me 
Everyone is now asleep #Me

As should you #Granny
It is nearly the WITCHING HOUR, you should be home #Granny
And whatever you do, DON'T GO beyond the graveyard #Granny

Why is that? #Me 
What happens beyond the graveyard after the witching hour? #Me 

It is said that on every October 31st, beyond the graveyard after the witching hour... #Granny
...monsters roam free! #Granny

The only way to be safe from them is if you're in your house #Granny

I shall be on my way then #Me
It is nearly the witching hour #Me

Take care child #Granny
-> END
