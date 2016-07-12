# Web API Spike

## Getting set up
Clone the project, open and build solution in Visual studio 2015

## Testing the endpoints
1. Download [Postman](https://chrome.google.com/webstore/detail/postman/fhbjgbiflinjbdggehcddcbncdddomop?hl=en) or your favorite REST client
2. Try running a GET request to `localhost:63497/api/connectionstring` and then
3. A POST request to `localhost:63497/api/echo` with the body
    ```
    {
        "message": "My test message"
    }
    ```
4. And the same request with an empty body `{ }` should return a list of errors

## What's included
[See the project wiki](https://github.com/rlgod/Web-API-Spike/wiki/Web-API-Spike) for more information about the code itself.
