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

generateGrid :: Int -> Int -> [[Int]]
generateGrid 1 b = [ 0 |x <- [1..b]]:[]
generateGrid a b = [ 0 |x <- [1..b]]:generateGrid (a-1) b

-- showGrid :: [[Int]] -> String
-- showGrid [] = ""
-- showGrid (x:xs) = ([ show a | a <- x]) ++ "\n" ++ showGrid xs

getRegexResults :: (String, String, String, [String]) -> (String, (Int, Int), (Int, Int))
getRegexResults (_,_,_,a) = (
  a!!0, 
  (read ((split (a!!1))!!0) :: Int, read ((split (a!!1))!!1) :: Int), 
  (read ((split (a!!2))!!0) :: Int, read ((split (a!!2))!!1) :: Int)
  )
  where split = wordsWhen (==',')

first (a,_,_) = a
second (_,a,_) = a
third (_,_,a) = a

testString :: String -> (String, (Int, Int), (Int, Int))
testString s = getRegexResults (s =~ "([turn on|toggle|turn off]+) ([0-9,]+) through ([0-9,]+)" :: (String, String, String, [String]))

switchBulb :: String -> Int -> Int
switchBulb o a
  | o == "turn on" = a + 1
  | o == "turn off" = if a - 1 < 0 then 0 else a - 1 
  | otherwise = a + 2

processLine :: String -> Int -> Int -> Int -> [Int] -> [Int]
processLine _ _ _ _ [] = []
processLine o x1 x2 p (x:xs) = (if p >= x1 && p <= x2 then switchBulb o x else x):processLine o x1 x2 (p+1) xs

processBoard :: String -> (Int, Int) -> (Int, Int) -> Int -> [[Int]] -> [[Int]]
processBoard _ _ _ _ [] = []
processBoard o (x1,y1) (x2,y2) l (x:xs) = (if l >= y1 && l <= y2 then processLine o x1 x2 0 x else x):processBoard o (x1,y1) (x2,y2) (l+1) xs

processOrder :: String -> [[Int]] -> [[Int]]
processOrder order a = processBoard (first (testString order)) (second (testString order)) (third (testString order)) 0 a

processContents :: [String] -> [[Int]] -> [[Int]]
processContents [] a = a
processContents (x:xs) a = processContents xs (processOrder x a)

countOn :: [[Int]] -> Int
countOn [] = 0
countOn (x:xs) = sum x + countOn xs
main = do
  let list = []
  handle <- openFile "./data.txt" ReadMode
  contents <- getlines handle
  putStrLn (show (countOn (processContents contents (generateGrid 1000 1000))))
  hClose handle   
