module Main (main) where
import System.IO  
import Text.Regex.TDFA

wordsWhen     :: (Char -> Bool) -> String -> [String]
wordsWhen p s =  case dropWhile p s of
                      "" -> []
                      s' -> w : wordsWhen p s''
                            where (w, s'') = break p s'

getlines :: Handle -> IO [String]
getlines h = hGetContents h >>= return . lines

-- getDiag with column and line for starting
getDiag :: [String] -> Int -> Int -> Bool -> String
getDiag s a b isRight
  | isRight && a < length (s!!0) && b < length s = ((s!!b)!!a):getDiag s (a+1) (b+1) isRight
  | not isRight && a >= 0 && b < length s = ((s!!b)!!a):getDiag s (a-1) (b+1) isRight
  | otherwise = []

getAllDiag :: [String] -> [String]
getAllDiag s = [getDiag s x 0 True | x <- [0..(length (s!!0) - 1)]] ++ [getDiag s 0 x True | x <- [1..(length s - 1)]] 
  ++ [getDiag s x 0 False | x <- reverse [0..(length (s!!0) - 1)]] ++ [getDiag s (length (s!!0) - 1) x False | x <- reverse [1..(length s - 1)]]

getVert :: [String] -> Int -> Int -> String
getVert s a b 
  | b < length s = ((s!!b)!!a):getVert s a (b+1)
  | otherwise = []

getAllVert :: [String] -> [String]
getAllVert s = [getVert s x 0 | x <- [0..(length (s!!0) - 1)]]

testXmas :: String -> Int
testXmas s = sum [length (getAllTextMatches (t =~ "XMAS") :: [String]) | t <- [s, reverse s]]

test = "MMXMASMSSAMXXXMASM"
main = do
  let list = []
  handle <- openFile "./data.txt" ReadMode
  contents <- getlines handle
  -- putStrLn (show (getAllDiag contents))
  -- putStrLn (show ([testXmas x | x <- getAllDiag contents]))
  -- putStrLn (show (sum [testXmas x | x <- contents] + sum [testXmas x | x <- getAllDiag contents]))
  putStrLn (show (sum [sum (map (\a -> testXmas a) x) | x <- [contents, getAllDiag contents, getAllVert contents]]))
  hClose handle   
