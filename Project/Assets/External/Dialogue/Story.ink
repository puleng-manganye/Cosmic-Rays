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
I got here late and missed Trick or Treating #Me 
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

=== Sizwe ===

Cozy fire you got on there #Me 
Hi, I'm Amandla #Me 

Careful that you don't get burnt #Sizwe 
Sizwe, nice to meet you Amandla :) #Sizwe 

What are you doing here this late and all by yourself? #Me 

Was going to ask you the same question #Sizwe 

You go first #Me 

Well... #Sizwe 
I'm from Moon City, on my way to Dreamland City to see my Granny #Sizwe 
It got dark and monsters started showing up everywhere #Sizwe
I got exhausted from running for my life so I'm waiting here where they can't get to me #Sizwe
I'll leave at dawn when it is safe #Sizwe 
You? #Sizwe

I'm from Dreamland City, on my way home at Moon City #Me

Ohh...Sounds dangerous on your own! #Sizwe 

I know, I don't know how I'm gonna get through the MISTY WOODS #Me 

Mhmm... #Sizwe 
I lost my Blaster, DJ and a few other items along the way #Sizwe 
Find those items, they'll come in handy #Sizwe

:D #Me 

The Blaster is fueled by and fires candy #Sizwe 
When you have enough candy, press Z to shoot at monsters #Sizwe 

Yay, less stomping #Me 
Is DJ... #Me

* a Disc Jockey? -> choice_303
+ a Double Jump? -> choice_304

=== choice_303 === 
... #Sizwe
No, it is the Double Jump #Sizwe
-> Amandla_2

=== choice_304 === 
... #Sizwe
Yes, it is the Double Jump #Sizwe
-> Amandla_2


=== Amandla_2 ===
It is an anti-gravity machine which will allow you to jump again in the air after a jump #Sizwe 

Woah, sounds cool #Me 

That's it from me, goodluck and I hope to see you again! #Sizwe 

Thanks, goodluck getting to Granny #Me 
-> DONE
