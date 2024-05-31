#!/bin/bash

# URL podstawowy API
BASE_URL="http://localhost:5000/api"

# Funkcja do testowania endpointu
test_endpoint() {
    local method=$1
    local endpoint=$2
    local data=$3
    local token=$4

    if [ "$method" == "GET" ]; then
        if [ -z "$token" ]; then
            curl -s -X GET "$BASE_URL$endpoint"
        else
            curl -s -X GET "$BASE_URL$endpoint" -H "Authorization: Bearer $token"
        fi
    elif [ "$method" == "POST" ]; then
        if [ -z "$token" ]; then
            curl -s -X POST "$BASE_URL$endpoint" -H "Content-Type: application/json" -d "$data"
        else
            curl -s -X POST "$BASE_URL$endpoint" -H "Content-Type: application/json" -H "Authorization: Bearer $token" -d "$data"
        fi
    elif [ "$method" == "PUT" ]; then
        if [ -z "$token" ];then
            curl -s -X PUT "$BASE_URL$endpoint" -H "Content-Type: application/json" -d "$data"
        else
            curl -s -X PUT "$BASE_URL$endpoint" -H "Content-Type: application/json" -H "Authorization: Bearer $token" -d "$data"
        fi
    elif [ "$method" == "DELETE" ]; then
        if [ -z "$token" ]; then
            curl -s -X DELETE "$BASE_URL$endpoint"
        else
            curl -s -X DELETE "$BASE_URL$endpoint" -H "Authorization: Bearer $token"
        fi
    fi
}

# Przykładowe dane do logowania
LOGIN_DATA='{"username": "admin", "password": "adminpassword"}'

# Logowanie i uzyskanie tokenu JWT
TOKEN=$(test_endpoint POST "/auth/login" "$LOGIN_DATA" | jq -r '.token')

# Testowanie endpointów
echo "Testing Menu Endpoints"
test_endpoint GET "/menu" "" "$TOKEN"
test_endpoint GET "/menu/1" "" "$TOKEN"
test_endpoint POST "/menu" '{"name": "New Item", "description": "Description", "price": 10.99, "category": "Category"}' "$TOKEN"
test_endpoint PUT "/menu/1" '{"id": 1, "name": "Updated Item", "description": "Updated Description", "price": 11.99, "category": "Updated Category"}' "$TOKEN"
test_endpoint DELETE "/menu/1" "" "$TOKEN"

echo "Testing Order Endpoints"
test_endpoint GET "/orders" "" "$TOKEN"
test_endpoint GET "/orders/1" "" "$TOKEN"
test_endpoint POST "/orders" '{"userId": 1, "items": [{"menuItemId": 1, "quantity": 2}]}' "$TOKEN"
test_endpoint PUT "/orders/1" '{"id": 1, "userId": 1, "items": [{"menuItemId": 1, "quantity": 3}]}' "$TOKEN"
test_endpoint DELETE "/orders/1" "" "$TOKEN"

echo "Testing User Endpoints"
test_endpoint GET "/users" "" "$TOKEN"
test_endpoint GET "/users/1" "" "$TOKEN"
test_endpoint POST "/users" '{"username": "newuser", "password": "password", "role": "User"}' "$TOKEN"
test_endpoint PUT "/users/1" '{"id": 1, "username": "updateduser", "password": "newpassword", "role": "User"}' "$TOKEN"
test_endpoint DELETE "/users/1" "" "$TOKEN"

echo "All endpoints tested."
