// SPDX-License-Identifier: MIT
pragma solidity ^0.8.0;

contract SimpleStorage {

    address public mainDeveloper; //Only the Developer can add users and merchants

    //Seller
    struct Seller {
        string Name;  //Sellers name
        uint32 Points;//Points held by seller
    }
    struct Customer {
        string Name; //Name of the customer
    }
    //Purchase transaction entry
    struct Transaction {
        string UserName;  //Name of buyer
        string SellerName;//Name of seller
        string ItemID;    //The item purchased
        uint32 Points;    //points transferred
    }
    //Consolidated points held by customer per seller
    //Customer can buy many items from a seller
    struct CustomerPoints {
        string UserName;  
        string SellerName;
        uint32 Points;     //Total points earned per seller
    }

    //Array list of sellers. This is preloaded from backend
    Seller[] public sellers;
    //Array of customers. This is preloaded from backend
    Customer[] public customers;

    //List of transactions done. updated for each purchase
    Transaction[] public Transactions;
    //Consolidated points per customer. Calculated at each purchase
    CustomerPoints[] public PointsTracker;

    constructor() {
        //This is called at contract installation time
        //The ID that sets up the contract is set as admin
        //Only admin can do maintenance of the block
        mainDeveloper = msg.sender;
    }

    /*--------------------------------------------------------
    The main operation on this smart contract
    Allows a customer to buy an item from the seller
    The amount of points to be transferred from seller to buyer
    is also passed as a parameter. The function checks whether
    the seller has enough points and debits the seller account
    Also the consolidated points earned by buyer is calculated
    ----------------------------------------------------------*/
    function buyItem(
        string memory customerName,
        string memory itemId,
        string memory sellerName,
        uint32 points
    ) public {
        //For this version we only allow a pre-approved 
        //set of buyers and sellers to participate

        //checks whether customer exists in the list
        require(existsCustomer(customerName), "Customer doesnt exist");
        //checks whether seller exists
        require(existsSeller(sellerName), "Seller doesnt exist");

        //get instance of Seller from list of sellers using name
        Seller storage s = getSeller(sellerName);

        if (points > 0) {
            require(
                s.Points >= points, //check whether seller has enough points
                "Seller doesnt have enough points." //Error message
            );
            s.Points -= points; //debit Sellers points

            //The loop checks whether a buyer+seller combo exists in the consolidated list
            //If found then update the points
            bool found = false;
            for (uint256 i = 0; i < PointsTracker.length; i++) {
                if (
                    compareString(PointsTracker[i].UserName, customerName) &&
                    compareString(PointsTracker[i].SellerName, sellerName)
                ) {
                    found = true;
                    PointsTracker[i].Points += points; //update buyers points
                    break;
                }
            }
            //if not found, buyer hasnt done past transaction with the seller
            //create a new entry
            if (!found) {
                PointsTracker.push(
                    CustomerPoints({
                        UserName: customerName,
                        SellerName: sellerName,
                        Points: points
                    })
                );
            }
        }

        //Also update the transaction list
        Transactions.push(
            Transaction({
                UserName: customerName,
                SellerName: sellerName,
                ItemID: itemId,
                Points: points
            })
        );
    }

    //Helper function to get customer block given a name
    function getCustomer(string memory _Name)
        internal
        view
        returns (Customer memory c)
    {
        string memory lowerName = _toLower(_Name);
        for (uint32 i = 0; i < customers.length; i++) {
            if (
                keccak256(abi.encodePacked(lowerName)) ==
                keccak256(abi.encodePacked(customers[i].Name))
            ) {
                return customers[i];
            }
        }
    }

   //Helper function to get seller block given a name
    function getSeller(string memory _Name)
        internal
        view
        returns (Seller storage s)
    {
        string memory lowerName = _toLower(_Name);
        for (uint32 i = 0; i < sellers.length; i++) {
            if (
                keccak256(abi.encodePacked(lowerName)) ==
                keccak256(abi.encodePacked(sellers[i].Name))
            ) {
                return sellers[i];
            }
        }
        return sellers[0];
    }

    function existsCustomer(string memory _Name) internal view returns (bool) {
        string memory lower = _toLower(_Name);
        for (uint32 i = 0; i < customers.length; i++) {
            if (
                keccak256(abi.encodePacked(lower)) ==
                keccak256(abi.encodePacked(customers[i].Name))
            ) {
                return true;
            }
        }
        return false;
    }

    function existsSeller(string memory _Name) internal view returns (bool) {
        string memory lower = _toLower(_Name);
        for (uint32 i = 0; i < sellers.length; i++) {
            if (
                keccak256(abi.encodePacked(lower)) ==
                keccak256(abi.encodePacked(sellers[i].Name))
            ) {
                return true;
            }
        }
        return false;
    }
    //Maintenance function to add new customers
    function addCustomer(string memory _Name) public {
        require(msg.sender == mainDeveloper, "Only admin can add new customer");
        require(existsCustomer(_Name) != true, "Customer already exists");
        customers.push(Customer({Name: _toLower(_Name)}));
    }

    //Maintenance function to add new seller and points 
    function addSeller(string memory _Name, uint32 points) public {
        require(msg.sender == mainDeveloper, "Only admin can add new Seller");
        require(existsSeller(_Name) != true, "Seller already exists");
        sellers.push(Seller({Name: _Name, Points: points}));
    }

    //helper function to return the list of all customers in the block
    function listCustomers() public view returns (Customer[] memory) {
        return customers;
    }
    //helper function to return the list of all sellers in the block
    function listSellers() public view returns (Seller[] memory) {
        return sellers;
    }
    //helper function to return the list of consolidated customer points in the block
    function listCustomerPoints()
        public
        view
        returns (CustomerPoints[] memory)
    {
        return PointsTracker;
    }
    //helper function to return the list of all transactions in the block
    function listTransactions() public view returns (Transaction[] memory) {
        return Transactions;
    }

    //solidity has minimal features compared to high level language as Java
    //hence we need some helper functions to compare names etc
    function _toLower(string memory str) internal pure returns (string memory) {
        bytes memory bStr = bytes(str);
        bytes memory bLower = new bytes(bStr.length);

        for (uint256 i = 0; i < bStr.length; i++) {
            // Uppercase character...
            if ((uint8(bStr[i]) >= 65) && (uint8(bStr[i]) <= 90)) {
                // So we add 32 to make it lowercase
                bLower[i] = bytes1(uint8(bStr[i]) + 32);
            } else {
                bLower[i] = bStr[i];
            }
        }
        return string(bLower);
    }

    function compareString(string memory s1, string memory s2)
        internal
        pure
        returns (bool)
    {
        string memory lower1 = _toLower(s1);
        string memory lower2 = _toLower(s2);

        return
            keccak256(abi.encodePacked(lower1)) ==
            keccak256(abi.encodePacked(lower2));
    }

    //helper function to update seller points. done from backend by admin
    function updateSellerPoints(string memory sellerName, uint32 points)
        public
    {
        require(msg.sender == mainDeveloper, "only admin can update points");
        require(existsSeller(sellerName), "seller doesnt exist");

        Seller storage s = getSeller(sellerName);
        s.Points += points;
    }

    //For demo purpose. To clear everything
    function clearEverything() public {
        require(msg.sender == mainDeveloper, "only admin can clear everything");
        delete sellers; 
        delete customers;
        delete Transactions;
        delete PointsTracker;
    }

    //Future implementation
    //User can use cash + points to buy item
    //
}
