import threading
import utils
import sys
from os import system
import os
from threading import Thread
import chess
import logging
import win32gui
import sys,time,random
from selenium import webdriver
from pynput.keyboard import Key, Controller
chop = webdriver.ChromeOptions()
chop.add_extension('{}\\chrome\\extension.crx'.format(os.getcwd()))
chop.add_experimental_option("excludeSwitches", ["enable-logging"])
driver = webdriver.Chrome(options=chop)
driver.get("https://www.chess.com/play/computer")
system('cls')
system('TITLE RayAI')
diff = input("What difficulty? Options: 200 Rating, 600 Rating, 1100 Rating, 2000 Grandmaster (Just put the number)\n")
match diff:
    case "200":
        rating = 1
    case "600":
        rating = 3
    case "1100":
        rating = 5
    case "2000":
        rating = 10
    case _:
        rating = 3
keyboard = Controller()
board = chess.Board()
botmoves = []
pawntable = [
    0, 0, 0, 0, 0, 0, 0, 0,
    5, 10, 10, -20, -20, 10, 10, 5,
    5, -5, -10, 0, 0, -10, -5, 5,
    0, 0, 0, 20, 20, 0, 0, 0,
    5, 5, 10, 25, 25, 10, 5, 5,
    10, 10, 20, 30, 30, 20, 10, 10,
    50, 50, 50, 50, 50, 50, 50, 50,
    0, 0, 0, 0, 0, 0, 0, 0]

knightstable = [
    -50, -40, -30, -30, -30, -30, -40, -50,
    -40, -20, 0, 5, 5, 0, -20, -40,
    -30, 5, 10, 15, 15, 10, 5, -30,
    -30, 0, 15, 20, 20, 15, 0, -30,
    -30, 5, 15, 20, 20, 15, 5, -30,
    -30, 0, 10, 15, 15, 10, 0, -30,
    -40, -20, 0, 0, 0, 0, -20, -40,
    -50, -40, -30, -30, -30, -30, -40, -50]

bishopstable = [
    -20, -10, -10, -10, -10, -10, -10, -20,
    -10, 5, 0, 0, 0, 0, 5, -10,
    -10, 10, 10, 10, 10, 10, 10, -10,
    -10, 0, 10, 10, 10, 10, 0, -10,
    -10, 5, 5, 10, 10, 5, 5, -10,
    -10, 0, 5, 10, 10, 5, 0, -10,
    -10, 0, 0, 0, 0, 0, 0, -10,
    -20, -10, -10, -10, -10, -10, -10, -20]

rookstable = [
    0, 0, 0, 5, 5, 0, 0, 0,
    -5, 0, 0, 0, 0, 0, 0, -5,
    -5, 0, 0, 0, 0, 0, 0, -5,
    -5, 0, 0, 0, 0, 0, 0, -5,
    -5, 0, 0, 0, 0, 0, 0, -5,
    -5, 0, 0, 0, 0, 0, 0, -5,
    5, 10, 10, 10, 10, 10, 10, 5,
    0, 0, 0, 0, 0, 0, 0, 0]

queenstable = [
    -20, -10, -10, -5, -5, -10, -10, -20,
    -10, 0, 0, 0, 0, 0, 0, -10,
    -10, 5, 5, 5, 5, 5, 0, -10,
    0, 0, 5, 5, 5, 5, 0, -5,
    -5, 0, 5, 5, 5, 5, 0, -5,
    -10, 0, 5, 5, 5, 5, 0, -10,
    -10, 0, 0, 0, 0, 0, 0, -10,
    -20, -10, -10, -5, -5, -10, -10, -20]

kingstable = [
    20, 30, 10, 0, 0, 10, 30, 20,
    20, 20, 0, 0, 0, 0, 20, 20,
    -10, -20, -20, -20, -20, -20, -20, -10,
    -20, -30, -30, -40, -40, -30, -30, -20,
    -30, -40, -40, -50, -50, -40, -40, -30,
    -30, -40, -40, -50, -50, -40, -40, -30,
    -30, -40, -40, -50, -50, -40, -40, -30,
    -30, -40, -40, -50, -50, -40, -40, -30]
def progressBar(count_value, total, suffix=''):
    bar_length = 10
    filled_up_Length = int(round(bar_length* count_value / float(total)))
    percentage = round(100.0 * count_value/float(total),1)
    bar = '=' * filled_up_Length + '-' * (bar_length - filled_up_Length)
    sys.stdout.write('[%s] %s%s ...%s\r' %(bar, percentage, '%', "{}\nThreads: {}".format(suffix, len(threading.enumerate()))))
    sys.stdout.flush()

def evaluate_board():
    if board.is_checkmate():
        if board.turn:
            return -9999 #you win 
        else:
            return 9999 #you lose
    if board.is_stalemate():
        return 0
    if board.is_insufficient_material():
        return 0

    wp = len(board.pieces(chess.PAWN, chess.WHITE))
    bp = len(board.pieces(chess.PAWN, chess.BLACK))
    wn = len(board.pieces(chess.KNIGHT, chess.WHITE))
    bn = len(board.pieces(chess.KNIGHT, chess.BLACK))
    wb = len(board.pieces(chess.BISHOP, chess.WHITE))
    bb = len(board.pieces(chess.BISHOP, chess.BLACK))
    wr = len(board.pieces(chess.ROOK, chess.WHITE))
    br = len(board.pieces(chess.ROOK, chess.BLACK))
    wq = len(board.pieces(chess.QUEEN, chess.WHITE))
    bq = len(board.pieces(chess.QUEEN, chess.BLACK))

    material = 100 * (wp - bp) + 320 * (wn - bn) + 330 * (wb - bb) + 500 * (wr - br) + 900 * (wq - bq)

    pawnsq = sum([pawntable[i] for i in board.pieces(chess.PAWN, chess.WHITE)])
    pawnsq = pawnsq + sum([-pawntable[chess.square_mirror(i)]
                           for i in board.pieces(chess.PAWN, chess.BLACK)])
    knightsq = sum([knightstable[i] for i in board.pieces(chess.KNIGHT, chess.WHITE)])
    knightsq = knightsq + sum([-knightstable[chess.square_mirror(i)]
                               for i in board.pieces(chess.KNIGHT, chess.BLACK)])
    bishopsq = sum([bishopstable[i] for i in board.pieces(chess.BISHOP, chess.WHITE)])
    bishopsq = bishopsq + sum([-bishopstable[chess.square_mirror(i)]
                               for i in board.pieces(chess.BISHOP, chess.BLACK)])
    rooksq = sum([rookstable[i] for i in board.pieces(chess.ROOK, chess.WHITE)])
    rooksq = rooksq + sum([-rookstable[chess.square_mirror(i)]
                           for i in board.pieces(chess.ROOK, chess.BLACK)])
    queensq = sum([queenstable[i] for i in board.pieces(chess.QUEEN, chess.WHITE)])
    queensq = queensq + sum([-queenstable[chess.square_mirror(i)]
                             for i in board.pieces(chess.QUEEN, chess.BLACK)])
    kingsq = sum([kingstable[i] for i in board.pieces(chess.KING, chess.WHITE)])
    kingsq = kingsq + sum([-kingstable[chess.square_mirror(i)]
                           for i in board.pieces(chess.KING, chess.BLACK)])

    eval = material + pawnsq + knightsq + bishopsq + rooksq + queensq + kingsq
    if board.turn:
        return eval
    else:
        return -eval
def alphabeta(alpha, beta, depthleft):
    bestscore = -9999
    if (depthleft == 0):
        return quiesce(alpha, beta)
    for move in board.legal_moves:
        board.push(move)
        score = -alphabeta(-beta, -alpha, depthleft - 1)
        board.pop()
        if (score >= beta):
            return score
        if (score > bestscore):
            bestscore = score
        if (score > alpha):
            alpha = score
    return bestscore


def quiesce(alpha, beta):
    stand_pat = evaluate_board()
    if (stand_pat >= beta):
        return beta
    if (alpha < stand_pat):
        alpha = stand_pat

    for move in board.legal_moves:
        if board.is_capture(move):
            board.push(move)
            score = -quiesce(-beta, -alpha)
            board.pop()

            if (score >= beta):
                return beta
            if (score > alpha):
                alpha = score
    return alpha


results = []
events = {}
lock = threading.Lock()

def selectmove(depth):
    try:
        #opening2.bin is bad
        move = chess.polyglot.MemoryMappedReader("{}/openings/openings1.bin".format(os.getcwd())).weighted_choice(board).move
        return move
    except:
        print('\n[#] Calculating next move  | Threads: {}'.format(len(threading.enumerate())))
        bestMove = chess.Move.null()
        bestValue = -99999
        alpha = -100000
        beta = 100000
        global results
        results = []
        global events
        events = {}
        threads = []
        for move in board.legal_moves:
            events[move] = threading.Event()
            thread = threading.Thread(target=evaluate_move, args=(move, depth, alpha, beta))
            threads.append(thread)
            thread.start()
        for move, event in events.items():
            event.wait()
        for i, move in enumerate(board.legal_moves):
            result = results[i]
            if result > bestValue:
                bestValue = result
                bestMove = move
            if result > alpha:
                alpha = result
        return bestMove
    
def evaluate_move(move, depth, alpha, beta):
    board.push(move)
    boardValue = -alphabeta(-beta, -alpha, depth - 1)
    board.pop()
    results.append(boardValue)

    
def get_bot_move(movenum):
    try:
        #thread = ThreadWithResult(target=selectmove, args=(rating,))
        #thread.start()
        #thread.join()
        #move = thread.result
        move = selectmove(rating)
        if board.is_legal(move):
            movenew(move, movenum)
        else:
            move = selectmove(rating)
            movenew(move, movenum)
    except:
        move = selectmove(rating)
        if board.is_legal(move):
            movenew(move, movenum)
        else:
            move = selectmove(rating)
            movenew(move, movenum)
def movenew(move, movenum):
    hwnd = win32gui.FindWindowEx(0,0,0, driver.title + " - Google Chrome")
    win32gui.SetForegroundWindow(hwnd)
    board.push(move)
    botmoves.append(move)
    for i, v in enumerate(str(move)):
        keyboard.press(v)
        keyboard.release(v)
    keyboard.press(Key.enter)
    keyboard.release(Key.enter)
def SeleniumFunction():
    input("Hit Enter once in game")
    system('cls')
    MOVE_NUM = 1

    # detect the color of the player
    PLAYER_COLOR = None
    white_king = utils.find_by_css_selector_persist(driver, "div[class=\"piece wk square-51\"]", wait=5)
    black_king = utils.find_by_css_selector_persist(driver, "div[class=\"piece bk square-58\"]", wait=5)
    if white_king.location['y'] > black_king.location['y']:
        PLAYER_COLOR = 'W'
    else:
        PLAYER_COLOR = 'B'

    while True:
        win_selector = f"div[data-cy=\"modal-game-over-header-title\"]"
        winalert = utils.find_by_css_selector(driver, win_selector)
        if winalert is not None:
            input("BOT Wins! (Hit Enter to Restart Script)")
            os.system("python main.py")
            print ("Restarting...")
            time.sleep(0.2) # 200ms to CTR+C twice
            quit()
        system("cls")
        for botmove in botmoves:
            print("BOTMOVE: {} | Threads: {}".format(botmove, len(threading.enumerate())))
        if board.turn == chess.WHITE and PLAYER_COLOR == 'W':
            get_bot_move(MOVE_NUM)
            MOVE_NUM += 1
            continue
        elif board.turn == chess.BLACK and PLAYER_COLOR == 'B':
            get_bot_move(MOVE_NUM)
            MOVE_NUM += 1
            continue
        # next move's css
        css_selector = f"div[data-ply=\"{MOVE_NUM}\"]"
        # wait for the player to make his move
        move = utils.find_by_css_selector_persist(driver, css_selector, wait=0.3).text
        win_selector = f"div[data-cy=\"modal-game-over-header-title\"]"
        winalert = utils.find_by_css_selector(driver, win_selector)
        if winalert is not None:
            input("BOT Wins! (Hit Enter to Restart Script)")
            os.system("python main.py")
            print ("Restarting...")
            time.sleep(0.2) # 200ms to CTR+C twice
            quit()
        # add the move to the board and stockfish engine
        try:
            board.push_san(move)
            #print("{} | {}".format(MOVE_NUM, move))
        except:
            print("Error! Board:\n | Threads: {}".format(len(threading.enumerate())))
            print(board)
            #print("\n Move: {}".format(move))
            continue
        MOVE_NUM +=1

selenium_thread = Thread(target=SeleniumFunction)
selenium_thread.start()
sys.exit()
while (True):
    if board.is_stalemate():
        print("Its a draw by stalemate, you suck")
    elif board.is_checkmate():
        if board.turn:
            print("Checkmate! You Win!")
        else:
            print("Checkmate! You Lose!")
        input("Please hit enter to exit!")
        exit()
    elif board.is_insufficient_material():
        print("Its a draw by insufficient material, i hate you")
    elif board.is_check():
        print("Check!")
    get_bot_move()