import System.IO  
import Control.Monad
import Data.List
import Data.String (IsString)

wordsWhen     :: (Char -> Bool) -> String -> [String]
wordsWhen p s =  case dropWhile p s of
                      "" -> []
                      s' -> w : wordsWhen p s''
                            where (w, s'') = break p s'

getlines :: Handle -> IO [String]
getlines h = hGetContents h >>= return . lines

toPairs :: String -> [(Char, Char)]
toPairs [] = []
toPairs (x:xs) = (x, h xs):toPairs xs
  where h [] = ' '
        h [x] = x
        h x = head x

triplicate :: String -> [String]
triplicate [] = []
triplicate (x:xs) = (x:(x:(x:""))):triplicate xs

noTriples :: String -> Bool
noTriples x = (length (filter (==True) [isInfixOf a x | a <- (triplicate ['a'..'z'])])) == 0

manyDoubles :: String -> Bool
manyDoubles a = length (filter (>1) [length (elemIndices x pp)|x <- pp]) > 0
  where pp = toPairs a

val = "qhhvhtzxzqqjkmpb"
main = do
  let list = []
  handle <- openFile "data.txt" ReadMode
  contents <- getlines handle
  -- putStrLn (show (noTriples "fzesxzzzcdfgzer"))
  putStrLn (show (manyDoubles val))
  hClose handle   
