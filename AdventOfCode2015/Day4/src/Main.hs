import Data.ByteString.Char8
import qualified Crypto.Hash.MD5 as MD5
import Data.ByteString.Base16
import Data.List

calcMd5 :: String -> String
calcMd5 a = Data.ByteString.Char8.unpack . Data.ByteString.Base16.encode $ MD5.hash (Data.ByteString.Char8.pack a)

base = "bgvyzdsv"

main :: IO ()
main = do
  Prelude.putStrLn (show (Data.List.length ((Data.List.takeWhile (np) [calcMd5 (base ++ (show x)) | x <- [1..]]))))
    where np x = not (Data.List.isPrefixOf "000000" x)
