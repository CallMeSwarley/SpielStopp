VAR GameToSell = "GameTitle"
VAR Price = 60
VAR PurchasePrice = 0
VAR boughtGame = false
VAR positiveEnding =false
Hello. Would you like to buy my copy of {GameToSell} for {Price}$
    + "Sounds like a deal!"
        ~PurchasePrice = Price
        ~boughtGame = true
        ~positiveEnding = true
        Wonderful!
    + "Maybe if you lower the price a bit..."
        ~Price-=5
        {Price<=0:
        ->purchasFailed
        - else:
        ->OtherPrice
        }
    + "No thank you"
        ->declined
        
===OtherPrice===
{RANDOM(1,3)>1:
    How about for {Price}$ then
  - else:
    ->purchasFailed
}
    + "Sounds like a deal!"
        ~PurchasePrice = Price
        ~boughtGame = true
        ~positiveEnding = true
        Wonderful!
        ->END
    + "Maybe if you lower the price a bit..."
        ~Price-=5
        {Price<=0:
        ->purchasFailed
        - else:
        ->OtherPrice
        }
    + "No thank you"
        ->declined
        

->END
===purchasFailed===
No thank you
->END

===declined===
Oh alright then
-> END