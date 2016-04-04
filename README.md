# Proeve
"Proeve van Bekwaamheid".

## Contribution
### Code Conventions
 * Enter Bracket
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

 * Sounds must be mp3 format.
 * SFX goes into the Art/SFX folder and BGM goes into Art/BGM.

 * Minimal game resolution is: 800x600.
 * Maximal game resolution is: 1920x1080.

## Contributors
Lieske Timmermans, Sharon Jansen, Angelina Mendes Duarte, Dejorden Moerman, Kerim Birlik, Jesse Stam, Petar Dimitrov.