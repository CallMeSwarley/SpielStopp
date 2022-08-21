VAR lookingFor = "GameTitle"
VAR followFlag = false
VAR sadFlag = false

Sorry, but do you have {lookingFor} in stock?
    * "Yes, of course! Follow me!"
    ~followFlag = true
        Thank you!
            -> DONE
    * "I'm sorry. I'm afraid we do not." 
    ~sadFlag = true
        That's a shame
            -> END