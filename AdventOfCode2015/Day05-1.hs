import System.IO  
import Control.Monad
import Data.List
val = ["ugknbfddgicrmopn"]

wordsWhen     :: (Char -> Bool) -> String -> [String]
wordsWhen p s =  case dropWhile p s of
                      "" -> []
                      s' -> w : wordsWhen p s''
                            where (w, s'') = break p s'

getlines :: Handle -> IO [String]
getlines h = hGetContents h >>= return . lines

noStr :: String -> Bool
noStr x = (not (isInfixOf "ab" x)) && (not (isInfixOf "cd" x)) && (not (isInfixOf "pq" x)) && (not (isInfixOf "xy" x))

toPairs :: String -> [(Char, Char)]
toPairs [] = []
toPairs (x:xs) = (x, h xs):toPairs xs
  where h [] = ' '
        h [x] = x
        h x = head x

hasDouble :: String -> Bool
hasDouble x = length (filter ok (toPairs x)) > 0
  where ok (a,b) = a == b

hasVowels :: String -> Bool
hasVowels x = length (filter ok x) >= 3
  where ok a = 'a' == a || 'e' == a  || 'i' == a  || 'o' == a  || 'u' == a 

main = do
  let list = []
  handle <- openFile "data.txt" ReadMode
  contents <- getlines handle
  putStrLn (show (length (filter hasVowels (filter hasDouble (filter noStr contents)))))
  hClose handle   
