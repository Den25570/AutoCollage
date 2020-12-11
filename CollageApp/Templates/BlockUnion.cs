using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollageApp.Templates
{
    public class BlockUnion : Block
    {
        public List<Block> Blocks;

        public BlockUnion(List<Block> blocks)
        {
            Blocks = blocks;
            foreach(Block block in Blocks)
            {
                block.blockUnion = this;
            }
        }

        ~BlockUnion()
        {           
            foreach (Block block in Blocks)
            {
                block.blockUnion = null;
            }
        }
    }
}
