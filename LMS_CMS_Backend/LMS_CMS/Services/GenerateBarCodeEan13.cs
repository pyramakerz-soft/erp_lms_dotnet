namespace LMS_CMS_PL.Services
{
    public class GenerateBarCodeEan13
    {
        public string GenerateEan13(string chaine)
        {
            // Take only the left 12 characters (if longer, otherwise use as-is)
            chaine = chaine.Length > 12 ? chaine.Substring(0, 12) : chaine;

            // Prepend digits until the length becomes 12.
            // If length is 11, prepend "1", otherwise prepend "0".
            while (chaine.Length < 12)
            {
                chaine = (chaine.Length == 11 ? "1" : "0") + chaine;
            }

            // Verify we have exactly 12 characters.
            if (chaine.Length != 12)
            {
                return "";
            }

            // Check that all characters are digits.
            foreach (var c in chaine)
            {
                if (!char.IsDigit(c))
                {
                    return "";
                }
            }

            int checksum = 0;

            // In the VB code, positions 2,4,...,12 (1-based) are summed first.
            // In C# (0-based), these are indices 1, 3, ..., 11.
            for (int i = 1; i < 12; i += 2)
            {
                checksum += (chaine[i] - '0');
            }
            checksum *= 3;

            // Next, add the values from positions 1,3,...,11 (1-based).
            // In C# these are indices 0, 2, ..., 10.
            for (int i = 0; i < 11; i += 2)
            {
                checksum += (chaine[i] - '0');
            }

            // Calculate check digit.
            int checkDigit = (10 - (checksum % 10)) % 10;
            chaine += checkDigit.ToString();

            return chaine;
        }
    }
}


/*
Detailed Example
Let’s walk through an example. Suppose the input is "12345678901" (which is 11 digits long):

Initial Input:

Input: "12345678901"
Length: 11
Padding to 12 Digits:

Since the length is 11, the rule is to prepend "1".
New string becomes: "1" + "12345678901" = "112345678901"
Now the string is exactly 12 digits long.
Validation:

The function checks each character. Since all characters ('1', '1', '2', …, '1') are digits, the function continues.
Checksum Calculation:

Step A: Sum of Even-Positioned Digits (1‑indexed):
These positions are 2, 4, 6, 8, 10, and 12.

For "112345678901", the digits at these positions (using 1‑based indexing) are:

Position 2: '1' → 1
Position 4: '3' → 3
Position 6: '5' → 5
Position 8: '7' → 7
Position 10: '9' → 9
Position 12: '1' → 1
Sum = 1 + 3 + 5 + 7 + 9 + 1 = 26
Multiply by 3 → 26 * 3 = 78

Step B: Sum of Odd-Positioned Digits (1‑indexed):
These positions are 1, 3, 5, 7, 9, and 11.

The corresponding digits are:

Position 1: '1' → 1
Position 3: '2' → 2
Position 5: '4' → 4
Position 7: '6' → 6
Position 9: '8' → 8
Position 11: '0' → 0
Sum = 1 + 2 + 4 + 6 + 8 + 0 = 21

Total Checksum:
Total checksum = 78 (even positions) + 21 (odd positions) = 99

Calculate the Check Digit:

Compute: checksum % 10 = 99 % 10 = 9
Then: (10 - 9) % 10 = 1 % 10 = 1
Final EAN‑13 Code:

Append the check digit 1 to the 12‑digit string:
"112345678901" + "1" = "1123456789011"
So, the final EAN‑13 code generated for the input "12345678901" is "1123456789011". 
*/