2D Game Engine
- Implements Facade, Decorator, Prototype, and Singleton design patterns.
- Facade is used to couple Scene instances for easy switching between Menu, Game, and Pause scenes using currentScene as the instance pointer
- Decorator is used to modify the Asteroid class so that when one is destroyed it's physics and image change
- Singleton is utilized to ensure only one instance of Game and provide near global access to that instance
- Prototype design pattern utilized to clone game Objects (characters, asteroids, etc.)