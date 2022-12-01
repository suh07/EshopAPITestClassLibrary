Feature: GET_Eshop

A short summary of the feature

@GET_RETRIEVE_ALL_ITEMS-DETAILS
Scenario:GET ALL ITEMS DETAILS
    Given User have been autheticated with email "admin@microsoft.com" and password "Pass@word1"
	When user get all items details
	Then User PUT API response shall be equal to "200"

@GET_RETRIEVE_A_SPECIFIC-ITEM-DETAILS
Scenario:GET A SPECIFIC ITEM DETAILS
    Given User have been autheticated with email "admin@microsoft.com" and password "Pass@word1"
	When a user enter the id "1" of an item
	Then User PUT API response shall be equal to "200"

@GET_RETRIEVE_AN_UNAVAILABLE_ITEM-DETAILS
Scenario:GET AN UNAVAILABLE ITEM DETAILS
    Given User have been autheticated with email "admin@microsoft.com" and password "Pass@word1"
	When a user enter the id "100" of an item
	Then User PUT API response shall be equal to "404"


