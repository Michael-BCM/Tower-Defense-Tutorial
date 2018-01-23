using UnityEngine;

public abstract class Animal
{
    protected string name;

    /// <summary>
    /// Every class that inherits from an abstract class has to implement the ABSTRACT methods. It does NOT have to implement the non-abstract methods. 
    /// </summary>
    protected abstract void MakeSound(); /// This is an abstract method within an abstract class, so it must be implemented in the inheriting classes.

    protected void Eat ()
    {
        ///This method is not abstract, 
        ///so despite the fact that it is within an abstract class, 
        ///the inheriting classes do not need to implement it. 

        Debug.Log("eats food");
    }
    
    public void Chase()
    {

    }
}

interface AnimalActions
{
    ///An interface is a CONTRACT.
    ///Every class that implements an interface must implement every method in that interface.

    void Chase();
}

public class Cat : Animal, AnimalActions
{
    /// <summary>
    /// A cat is NOT an object until it is instantiated. Until then, it is just a class. 
    /// </summary>

    protected override void MakeSound ()
    {

    }
}

public class Dog : Animal, AnimalActions
{
    protected override void MakeSound()
    {

    }
}

public class AAnimal
{
    public class ACat : AAnimal
    {
        public class ASmallCat : ACat
        {

        }

        public class ALargeCat : ACat
        {

        }

        public ASmallCat smallCat = new ASmallCat();
        public ALargeCat largeCat = new ALargeCat();
    }

    public ACat cat = new ACat();



}

public class MainClass
{
    AAnimal animal = new AAnimal();

    public void Main ()
    {
        


    }
}



