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

val = getNumbers( wordsWhen (==' ') "7 6 9 2 1")
main = do  
        let list = []
        handle <- openFile "data.txt" ReadMode
        contents <- getlines handle
        putStrLn (show (nbTrue [isSafe (getNumbers( wordsWhen (==' ') x)) | x <- contents]))
        -- putStrLn (show (val))
        -- putStrLn (show (tail val))
        -- putStrLn (show (head val))
        -- putStrLn (show (isAllDown (tail val) (head val)))
        hClose handle   
