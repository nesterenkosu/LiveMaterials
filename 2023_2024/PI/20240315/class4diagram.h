abstract class Animal {
	private:
		int temperatue;
		int blood_pressure;
	public:
		int age;
		char name[];
		void Go();
		void Eat();
};

class Dog: Animal {
	public:
		Tail tail;
		Owner *owner;
		void Bark();
}

class Tail() {
	public:
		int length();
		void Wave();
}

class Owner {
	public:
		char name[255];
}

class Cat: Animal {
	public:
		void Meaw();
}