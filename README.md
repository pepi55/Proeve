# Proeve
"Proeve van Bekwaamheid".

## Contribution
### Code Conventions
 * Enter Bracket for C#
 * Bracket enter for HTML5/JS
 * Folders are UpperCamelCase.
 * __No one letter variables.__ (Except in for loops.)
 * __No public variables, use properties.__

 Auto generated:
 ```C#
 public int Foo { get; set; }
 ```

 User generated:
 ```C#
 private int bar;
 public int Bar
 {
     get
     {
         return value;
     }

      set
     {
         bar = value;
     }
 }
 ```
 * Initialization on one line. (i.e. `[SerializeField] private int variable;`)
 * Unity methods are protected. (i.e. `protected void Awake()`)
 * `Start()` is for initialization, `Awake()` is for assigning variables.
 * Use underscores before private variable names to increase readability.
 * One `namespace` for entire project.
 * Utils use a seperate `namespace`.
 * Warnings should be treated as errors.
 * Always commit a working build.
 * Always add protection level to method/variable.
 * Use events if you want a static function except if it has to be called every frame.
 * Use SceneController when loading scenes.

#### HTML5/JS naming conventions.
 * lowerCamelCase names (applies to folders, classes, variables, etc.).
 * Names are in English.
 * Each class has its own seperate JS file named after itself (i.e.: wall class would be wallClass.js).

### How to use issue tracker
 * Note the engine for the issue in the header.
 * Describe the bug as accurate as possible.
 * Important: Add the steps to reproduce the problem.
 * Bugs that are not fixable within 5 minutes are to be put in the tracker.

### Art conventions
 * Art assets are to be imported in Unity.
 * If the art is faulty, it is to be sent back to the artists so they can fix it.

 * Place the art assets in the correct subfolder (i.e. Art/Character/CharacterName).
 * If the folder does not exist create a new one.
 * Naming conventions are UpperCamelCase (no spaces, dashes, etc.).
 * Names are in English.

 * Sprites and spritesheets must be in png format.
 * The png's maximum size must not exceed 1024x1024.
 * Art may not contain empty spaces (i.e. if the picture is 50x50 don't export it as 1024x1024).
 * Make the sizes as a power of 2 as much as possible.

 * Ball images are 128x128 pixels.

 * Sounds must be mp3 format.
 * SFX goes into the Art/SFX folder and BGM goes into Art/BGM.

 * Minimal game resolution is: 800x600.
 * Maximum game resolution is: 1920x1080.

#### JS art conventions
 * Character's png's should be named: "image.IMAGE_NUMBER.png", where IMAGE_NUMBER is the number of the character made.

### Responsibilities
Each artist is responsible for importing his/her own work in Unity.
Lieske and Kerim are responsible for keeping track of the scrum board.
Each programmer is responsible for coding in the code conventions.
Petar is responsible for issues with git.
Dejorden is responsible for art issues.

## Contributors
### Artists
Lieske Timmermans,
Sharon Jansen,
Angelina Mendes Duarte,
Dejorden Moerman.

### Developers
Kerim Birlik,
Jesse Stam,
Petar Dimitrov.