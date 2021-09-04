using System.Numerics;
using Nethereum.Web3;
using UnityEngine;

public class EthereumBalanceOfExample : MonoBehaviour
{
    public string network = "mainnet"; // mainnet ropsten kovan rinkeby goerli 
    public string account = "0xb8F0FA30B7691F2E2B0a4DD8A574689636341348";

    async void Start()
  {
    Ethereum ethereum = new Ethereum(network);
    BigInteger wei = await ethereum.BalanceOf(account);
    print("wei: " + wei);
    print("eth: " + Web3.Convert.FromWei(wei));
  }
}
