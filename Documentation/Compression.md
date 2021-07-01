# Compression

## Gzip

```{.cs}

var myByteArray = new byte[] {1, 2, 3, 5, 6, 7 };
var myCompressedByteArray = Gzip.Compress(myByteArray);

var myString = "my string";
var myCompressedStringASCII = Gzip.Compress(myString, Encoding.ASCII);
var myCompressedStringUTF8 = Gzip.Compress(mystring);
    
// Will return the compressed byte array
var myCompressedByteArray = Gzip.Compress(myByteArray);

// Will return the compressed string as byte array, using ASCII for encoding
var myCompressedStringASCII = Gzip.Compress(myString, Encoding.ASCII);

// Will return the compressed string as byte array, using UTF8 for encoding
var myCompressedStringUTF8 = Gzip.Compress(myString);

// Will return the decompressed byte array
var myDecompressedByteArray = Gzip.Decompress(myCompressedByteArray);

// Will return the decompressed string using the provided encoding
var myDecompressedStringASCII = Gzip.DecompressToString(myCompressedStringASCII, Encoding.ASCII);

// Will return the decompressed string using the default encoding
var myDecompressedStringUTF8 = Gzip.DecompressToString(myCompressedStringUTF8);

```

\ref SCM.SwissArmyKnife.Compression.Gzip "View More Documentation"
