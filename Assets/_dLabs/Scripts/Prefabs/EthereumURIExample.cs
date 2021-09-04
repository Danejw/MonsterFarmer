using System.Numerics;
using Nethereum.Web3;
using UnityEngine;

public class EthereumURIExample : MonoBehaviour
{
    string chain = "ethereum";
    public string network = "ropsten"; // mainnet ropsten kovan rinkeby goerli
    string contract = "0xbDF2d708c6E4705824dC024187cd219da41C8c81";
    public string account = "0xb8F0FA30B7691F2E2B0a4DD8A574689636341348";

    string tokenId = "2";

    public void SearchAddress()
    {
        GetERC115Balance(account);
        GetERC721Balance(account);
        GetWei(account);
    }

    public async void GetERC115Balance(string address)
    {
        BigInteger balanceOf = await ERC1155.BalanceOf(chain, network, contract, address, tokenId);
        print("Balance of ERC1155 :" + balanceOf);
    }

    public async void GetERC721Balance(string address)
    {
        string uri = await ERC721.URI("ethereum", network, address, tokenId); // .URI("ethereum", network, account, "0");

        print("ERC721 URI : " + uri);
    }

    public async void GetWei(string address)
    {
        Ethereum ethereum = new Ethereum(network);
        BigInteger wei = await ethereum.BalanceOf(address);

        print("Wei : " + wei);
    }
}
