
Feature: Login

Scenario: Successful login with valid credentials
  Given I navigate to the OrangeHRM login page
  When I login with username "Admin" and password "admin123"
  Then Verify whether landed on Dashboard page or not