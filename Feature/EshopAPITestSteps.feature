Feature: EshopAPITestSteps

A shorturttd  summary of the feature

@GET_API
Scenario:GET API - Get all items details
	When user get all items details

@POST_API
Scenario:POST API add an item
	When User add a new item

@GET_API_WITH_INPUT
Scenario:GET API - Get a specific item
	When a user enter the id "1" of an item

@POST_API_Authenticate
Scenario:POST API - Authenticate user 
	When action is POST authenticate user

@POST_API_Delete
Scenario:Delete API - Permanently delete a specific item 
	When User enter the id "53" of an item

@POST_API_PUT
Scenario: PUT API - Update a specific item's details
	When User will update an item details


@POST_ADD_A_SPECIFIC-ITEM
Scenario:ADD A SPECIFIC ITEM DETAILS
    Given User have been autheticated with email "admin@microsoft.com" and password "Pass@word1"
	When user enter the item details with the following 
	| catalogBrandId | catalogTypeId | description		 | name              | price  |
	|         2      |       2       |   Tshirt-oranz    |   Tshirt-redOranz |  220.0 |
	Then User response shall be equal to "201"

@PUT_UPDATE_A_SPECIFIC-ITEM
Scenario:UPDATE A SPECIFIC ITEM DETAILS
    Given User have been autheticated with email "admin@microsoft.com" and password "Pass@word1"
	When user enter the item details including id 
	|  id| catalogBrandId | catalogTypeId | description  | name            | price |
	|  71| 2              | 2             | Tshirt-oranz | Tshirt-redOranz | 250.0 |
	Then User PUT API response shall be equal to "200"

@GET_RETRIEVE_A_SPECIFIC-ITEM-DETAILS
Scenario:GET A SPECIFIC ITEM DETAILS
    Given User have been autheticated with email "admin@microsoft.com" and password "Pass@word1"
	When a user enter the id "1" of an item
	Then User PUT API response shall be equal to "200"

@DELETE_A_SPECIFIC-ITEM
Scenario:DELETE A SPECIFIC ITEM DETAILS
    Given User have been autheticated with email "admin@microsoft.com" and password "Pass@word1"
	When User enter the id "61" of an item
	Then User PUT API response shall be equal to "200"

@GET_RETRIEVE_ALL_ITEMS-DETAILS
Scenario:GET ALL ITEMS DETAILS
    Given User have been autheticated with email "admin@microsoft.com" and password "Pass@word1"
	When user get all items details
	Then User PUT API response shall be equal to "200"




	








