Project RouletteWebAPI
---------------------------------------------
CLEAN CODE API PROJECT
CREATED BY: JHOHAN EDUARDO CAPADOR DIAZ 
---------------------------------------------
IMPORTANT: It's important to install redis for the project

Localization Project:
Steps to testing
1 - Metod Post with route api/Roulette/CreateRoulette-> Input none -> Output json  ResultActionGeneric<Roulette>

2 - Metod Put with route api/Roulette/StartRoulette  -> Input [FromQuery]  string idRoulette -> Output json  ResultActionGeneric<Roulette>

3 - Metod POST with route api/Roulette/CreateBet/{userId} -> input string UserId Header and [FromBody] Bet json -> Output json ResultActionGeneric<Bet>

4 - Metod GET with route api/Roulette/CloseBet/ -> Input [FromQuery] string idRoulette -> Output  json ResultActionGeneric<List<Bet>>

5 - Metod GET with route api/Roulette/ListRouletteCreate/ -> Input none ->  Output json ResultActionGeneric<List<Roulette>>
