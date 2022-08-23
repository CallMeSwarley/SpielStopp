VAR lookingFor = "Default Title"
VAR positiveEnding = false
VAR sadFlag = false

Sorry, but do you have {lookingFor} in stock?
    * "Yes, of course! Follow me!"
    ~positiveEnding = true
        Thank you!
            -> DONE
    * "I'm sorry. I'm afraid we do not." 
    ~positiveEnding = false
        That's a shame
            -> END