using System.Collections.Generic;
using UnityEngine;

/**

    Digit indexes scheme:
              0
             -----
            |     |
       1 -> |  3  | <- 2
             -----
       4 -> |     | <- 5
            |  6  |
             -----

    =>
    Since we need only 7 bits - C#'s 'byte' (8-bits) type is sufficient here.
    (we can use 'short' (16-bits) type f.e. to expand this to support digits which show letters etc)

    BINARY MASKS:
        1 means ON (green material), 0 means OFF (gray material)
                    
                    7 6 5 4 3 2 1 0    INT  HEX

        Number 0 -  0 1 1 1 0 1 1 1   (119, 0x77)
        
        Number 1 -  0 0 1 0 0 1 0 0   (36,  0x24)
        
        Number 2 -  0 1 0 1 1 1 0 1   (93,  0x5D)
        
        Number 3 -  0 1 1 0 1 1 0 1   (109, 0x6D)
        
        Number 4 -  0 0 1 0 1 1 1 0   (46,  0x2E)
        
        Number 5 -  0 1 1 0 1 0 1 1   (107, 0x6B)
        
        Number 6 -  0 1 1 1 1 0 1 1   (123, 0x7B)
        
        Number 7 -  0 0 1 0 0 1 0 1   (37,  0x25)
        
        Number 8 -  0 1 1 1 1 1 1 1   (127, 0x7F)
        
        Number 9 -  0 1 1 0 1 1 1 1   (111, 0x6F)
*/

public class DigitController : MonoBehaviour
{
    private const int minValue = 0;
    private const int maxValue = 9;

    private const byte DIGIT_MASK_OFF   = 0x00;
    private const byte DIGIT_MASK_0     = 0x77;
    private const byte DIGIT_MASK_1     = 0x24;
    private const byte DIGIT_MASK_2     = 0x5D;
    private const byte DIGIT_MASK_3     = 0x6D;
    private const byte DIGIT_MASK_4     = 0x2E;
    private const byte DIGIT_MASK_5     = 0x6B;
    private const byte DIGIT_MASK_6     = 0x7B;
    private const byte DIGIT_MASK_7     = 0x25;
    private const byte DIGIT_MASK_8     = 0x7F;
    private const byte DIGIT_MASK_9     = 0x6F;

    [SerializeField] private SegmentController digit0;
    [SerializeField] private SegmentController digit1;
    [SerializeField] private SegmentController digit2;
    [SerializeField] private SegmentController digit3;
    [SerializeField] private SegmentController digit4;
    [SerializeField] private SegmentController digit5;
    [SerializeField] private SegmentController digit6;

    private List<SegmentController> segments;
    private List<byte> digitMasks = new List<byte>();

    private void Start() 
    {
        segments = new List<SegmentController>()
        {
            digit0, digit1, digit2, digit3, digit4, digit5, digit6
        };
        digitMasks = new List<byte>()
        {
            DIGIT_MASK_0, DIGIT_MASK_1, DIGIT_MASK_2, DIGIT_MASK_3, DIGIT_MASK_4,
            DIGIT_MASK_5, DIGIT_MASK_6, DIGIT_MASK_7, DIGIT_MASK_8, DIGIT_MASK_9
        };
    }

    public void SetDigitValue(int newValue) 
    {
        if(newValue < minValue || newValue > maxValue || newValue > digitMasks.Count) 
        {
            SetAllSegmentsWithMask(DIGIT_MASK_OFF);
        }
        else 
        {
            SetAllSegmentsWithMask(digitMasks[newValue]);
        }
    }

    private void SetAllSegmentsWithMask(byte mask)
    {
        foreach(SegmentController segment in segments) 
        {
            // Since attribute Range[min, max] doesn't allow creation of non-ordered ranges, like 1, 2, 4, 8, 16 ... (so powers of 2 in our case),
            // bitIndex used as a value of power for 2 here.
            int tag = (int) Mathf.Pow(2, segment.bitIndex);
            bool isBitSet = (tag & mask) > 0;
            segment.SetIsOn(isBitSet);
        }
    }
}
