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

toTriple :: String -> Char -> Char -> [String]
toTriple [] _ _ = []
toTriple (x:xs) a b = (a:(b:(x:""))):toTriple xs b x

toPairs :: String -> Char -> [String]
toPairs [] _ = []
toPairs (x:xs) a = (a:(x:"")):toPairs xs x

triplicate :: String -> [String]
triplicate [] = []
triplicate (x:xs) = (x:(x:(x:""))):triplicate xs

noTriples :: String -> [Char] -> Bool
noTriples x c = (length (filter (==True) [isInfixOf a x | a <- (triplicate c)])) == 0

manyDoubles :: String -> [String]
manyDoubles a = nub (filter (\x -> length x > 0) [if length (elemIndices x pp) > 1 then x else [] |x <- pp])
  where 
    pp = toPairs (drop 1 a) (a!!0)

finalManyDoubles :: String -> Bool
finalManyDoubles x = if length (filter same md) > 0 then noTriples x [a!!0 |a <- filter same md] && length md > 0 else length md > 0
  where 
    md = manyDoubles x
    same a = a!!0 == a!!1

finalManyDoubles2 :: String -> Bool
finalManyDoubles2 x = length (filter (== True) [if a!!0 == a!!1 then noTriples x [a!!0] else True | a <- md]) > 0
  where 
    md = manyDoubles x

oneInTheMiddle :: String -> Bool
oneInTheMiddle x = length (filter (== True) [a!!0 == a!!2 && a!!1 /= a!!0 | a <- toTriple (drop 2 x) (x!!0) (x!!1)]) > 0

val = "dddertouiuer"
main = do
  let list = []
  handle <- openFile "data.txt" ReadMode
  contents <- getlines handle
  -- putStrLn (show (filter noTriples contents))
  -- putStrLn (show (filter (\a -> a!!0 == a!!1) (manyDoubles val)))
  -- putStrLn (show (manyDoubles val))
  -- putStrLn (show (finalManyDoubles2 val))
  -- putStrLn (show (filter finalManyDoubles contents))
  -- putStrLn (show (filter oneInTheMiddle contents))
  -- putStrLn (show (filter finalManyDoubles (filter oneInTheMiddle contents)))
  putStrLn (show (length (filter finalManyDoubles2 (filter oneInTheMiddle contents))))
  hClose handle   
