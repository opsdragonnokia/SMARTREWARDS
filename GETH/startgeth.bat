RD /S /Q %~dp0\devChain\geth
geth  --datadir=devChain init genesis_clique.json
geth --nodiscover --rpc  --datadir=devChain --networkid=1337 --rpccorsdomain "*" --mine  --ws --rpcapi "eth,web3,personal,net,miner,admin,debug" --rpcaddr "0.0.0.0" --http.addr "0.0.0.0" --http.port 8545 --allow-insecure-unlock --unlock 0x12890d2cce102216644c59daE5baed380d84830c --password "pass.txt" --verbosity 0 console
