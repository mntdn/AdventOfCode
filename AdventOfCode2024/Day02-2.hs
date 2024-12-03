import System.IO  
import Control.Monad
import Data.List

wordsWhen     :: (Char -> Bool) -> String -> [String]
wordsWhen p s =  case dropWhile p s of
                      "" -> []
                      s' -> w : wordsWhen p s''
                            where (w, s'') = break p s'

getlines :: Handle -> IO [String]
getlines h = hGetContents h >>= return . lines

getNumbers :: [String] -> [Int]
getNumbers [] = []
getNumbers (x:xs) = (read x :: Int):getNumbers xs

isUp :: Int -> Int -> Bool
isUp a b = a < b && b - a >= 1 && b - a <= 3

isAllUp :: [Int] -> Int -> [Bool]
isAllUp [] _ = []
isAllUp (x:xs) b = isUp b x:isAllUp xs x

isDown :: Int -> Int -> Bool
isDown a b = a > b && a - b >= 1 && a - b  <= 3

isAllDown :: [Int] -> Int -> [Bool]
isAllDown [] _ = []
isAllDown (x:xs) b = isDown b x:isAllDown xs x

isSafe :: [Int] -> Bool
isSafe x 
    | isUp (x!!0) (x!!1) = False `notElem` (isAllUp (tail x) (x!!0))
    | isDown (x!!0) (x!!1) = False `notElem` (isAllDown (tail x) (x!!0))
    | otherwise = False

nbTrue :: [Bool] -> Int
nbTrue [] = 0
nbTrue (x:xs) = (if x == True then 1 else 0) + (nbTrue xs)

removeElem :: [Int] -> Int -> [Int]
removeElem a b = take b a ++ drop (b+1) a

trySafe :: [Int] -> [Int] -> [Bool]
trySafe _ [] = []
trySafe a (x:xs) = isSafe (removeElem a x):trySafe a xs

isTrulySafe :: [Int] -> Bool
isTrulySafe x
    | isSafe x = True
    | otherwise = (nbTrue (trySafe x [0..(length x)])) > 0

val = getNumbers( wordsWhen (==' ') "7 6 9 2 1")
main = do  
        let list = []
        handle <- openFile "data.txt" ReadMode
        contents <- getlines handle
        putStrLn (show (nbTrue [isTrulySafe (getNumbers( wordsWhen (==' ') x)) | x <- contents]))
        hClose handle   
