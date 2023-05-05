<h3 align="center">Chess Bot</h3>
<p align="center">a bot using minimax, negamax, alpha-beta pruning, and a custom algorithm</p>
<p align="center">designed to be a fun challenge for any level</p>

<h4 align="center">Theory</h5>
<p align="center">The bot calulcates the best positions by giving every "situation" or space a piece can go a value depending on what piece is there. Then it calculates the max amount of pieces on the board at a given time. Thus the evaluation function of the board is</p>
<div align="center"><img src="https://i.ibb.co/Ht6fghh/image.png" alt="Evaluation function flowchart"></div>
<p align="center">Then we use minimax and negamax to calculate the next best move, shown here.</p>
<div align="center"><img src="https://i.ibb.co/KKvRVMg/image.png" alt="Search function flowchart"></div>
<p align="center">Now all that is left is alphabeta and quiesce to remove a lot of iterations in the search.</p>
<h2 align="center"><a href="https://github.com/YungSamzy/chess-ai/releases/latest">PLAY NOW</a></h2>
