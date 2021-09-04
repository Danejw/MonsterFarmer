using System.Numerics;
using UnityEngine;

public class EthereumBlockHeightCounter : MonoBehaviour
{
    public string network = "ropsten"; // mainnet ropsten kovan rinkeby goerli
    BigInteger blockHeight;
    Ethereum ethereum;

    async void Start()
    {
        ethereum = new Ethereum(network);
        blockHeight = await ethereum.BlockHeight();
        print("Block Height : " + blockHeight);
    }




    /*
    private async void Update()
    {
        if (blockHeight != await ethereum.BlockHeight())
        {
            blockHeight = await ethereum.BlockHeight();
            print(blockHeight);
        }
    }
    */
}
