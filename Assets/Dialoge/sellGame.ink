VAR GameToSell = "GameTitle"
VAR Price = 60
VAR positiveEnding = false
Hello. Would you like to buy my copy of {GameToSell} for {Price}$
    * "Sounds like a deal
        ~positiveEnding = true
        Wonderful!
        -> END
    * "No thank you"
        -> END
        
        