rem RD /S /Q %~dp0\nudd1\geth 
rem just clear the cache of GETH
rem geth --datadir nudd1 account new --password pass.txt
REM 0x7eD2bE1C8ff476a916c64f445CC49f5113283Bfa
REM 0x7eD2bE1C8ff476a916c64f445CC49f5113283Bfa:   Private 0x49db19f96c3881aceaae0893916a2b5d99cdeb95d9f77067f41e5ae8b03a2702
REM contract address 0x849a412a98e4340814678e9b2a7270330ae2e9c3

REM geth  --datadir=nudd1 init gens2.json
REM geth --nodiscover --rpc  --datadir=chain --dev --mine --rpccorsdomain "*" --rpcapi "eth,web3,personal,net,miner,admin,debug" --rpcaddr "0.0.0.0" --http.addr "0.0.0.0" --http.port 8545 --http.corsdomain "moz-extension://aaac9e04-2a0d-45a8-bb45-7ad026ae5536" --allow-insecure-unlock --verbosity 0 console
REM --unlock "0x7eD2bE1C8ff476a916c64f445CC49f5113283Bfa" --password pass.txt 
rem --unlock "0x7eD2bE1C8ff476a916c64f445CC49f5113283Bfa" --password pass.txt
rem --miner.gasprice 0

REM 0x4be97a54a8d89d0b1bd6a8ddedd8306401fcd44bdd41bdf8b8aeca2a6c66c85b

REM eth.sendTransaction({from: eth.coinbase, to: eth.accounts[1], value: web3.toWei(50, "ether")})
