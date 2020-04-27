[![Build Status](https://bmurdoch.visualstudio.com/uni-resolver-driver-did-ion/_apis/build/status/decentralized-identity.uni-resolver-driver-did-ion?branchName=master)](https://bmurdoch.visualstudio.com/uni-resolver-driver-did-ion/_build/latest?definitionId=1&branchName=master) ![Azure DevOps tests](https://img.shields.io/azure-devops/tests/bmurdoch/uni-resolver-driver-did-ion/1) ![Azure DevOps coverage](https://img.shields.io/azure-devops/coverage/bmurdoch/uni-resolver-driver-did-ion/1)

# uni-resolver-driver-did-ion
Universal Resolver Driver for Identity Overlay Network (ION) DIDs

![DIF Logo](https://raw.githubusercontent.com/decentralized-identity/universal-resolver/master/docs/logo-dif.png)

# Universal Resolver Driver: driver:ion

This is a [Universal Resolver](https://github.com/decentralized-identity/universal-resolver/) driver for **did:ion** identifiers.

## Specifications

* [Decentralized Identifiers](https://w3c.github.io/did-core/)
* [DID Method Specification](https://github.com/decentralized-identity/ion)

## Example DIDs

```
did:ion:test:EiDRPAETJeLINVPwpSqboRYmKyWh8JbM2FVHmaPdLEw6ng?-ion-initial-state=eyJkZWx0YV9oYXNoIjoiRWlEa2d5eHRxNkZTVUNyV19MM2Jzby05X3hIazlETUI2TU1ROUI0U25qTVlPQSIsInJlY292ZXJ5X2tleSI6eyJrdHkiOiJFQyIsImNydiI6InNlY3AyNTZrMSIsIngiOiJBYUt1a3BDMFhBeW9qRUtpVVdXZS1KeUxYbzBIZzExY3FFNzc0T2xRVjZZIiwieSI6Imt5dXViNDBXaTFoMENMVWFQZ3h5QXNFd1hpWlFLX2RXUXVzYjhZTllzMW8ifSwicmVjb3ZlcnlfY29tbWl0bWVudCI6IkVpQ3FBajBUanFoaU5LU1hWNllfTThzdFZFODNHVmltLXNwMHhzVERBTERaTlEifQ.eyJ1cGRhdGVfY29tbWl0bWVudCI6IkVpQlZMOEVDZ2ViaTUwSVE3SzVETzRxOUIxaGd0akxGWjhnWEJ0RWpiVjFxV0EiLCJwYXRjaGVzIjpbeyJhY3Rpb24iOiJyZXBsYWNlIiwiZG9jdW1lbnQiOnsicHVibGljS2V5cyI6W3siaWQiOiJzaWduaW5nS2V5IiwidHlwZSI6IlNlY3AyNTZrMVZlcmlmaWNhdGlvbktleTIwMTkiLCJqd2siOnsia3R5IjoiRUMiLCJjcnYiOiJzZWNwMjU2azEiLCJ4IjoiY0Y0ZFFOXzUxZGxOOXkzdml1REJWM2hOcm9zbHpha2dKYW9OZzJTcFNVYyIsInkiOiJVdWdtcVljZHJyWlBZdVZ1d0J6dENHRUtEYlZhbzE0OFd4ZjM5U0pOVTRRIn0sInVzYWdlIjpbIm9wcyIsImF1dGgiLCJnZW5lcmFsIl19XSwic2VydmljZUVuZHBvaW50cyI6W3siaWQiOiJzZXJ2aWNlRW5kcG9pbnRJZDEyMyIsInR5cGUiOiJzb21lVHlwZSIsInNlcnZpY2VFbmRwb2ludCI6Imh0dHBzOi8vd3d3LnVybC5jb20ifV19fV19
```

## Build and Run (Docker)
```
docker build --pull --rm -f "src\Dockerfile" -t uniresolverdriverdidion:latest "src"
docker run --rm -d  -p 5000:5000/tcp uniresolverdriverdidion:latest
curl -X GET http://localhost:5000/1.0/identifiers/did:ion:EiDRPAETJeLINVPwpSqboRYmKyWh8JbM2FVHmaPdLEw6ng?-ion-initial-state=eyJkZWx0YV9oYXNoIjoiRWlEa2d5eHRxNkZTVUNyV19MM2Jzby05X3hIazlETUI2TU1ROUI0U25qTVlPQSIsInJlY292ZXJ5X2tleSI6eyJrdHkiOiJFQyIsImNydiI6InNlY3AyNTZrMSIsIngiOiJBYUt1a3BDMFhBeW9qRUtpVVdXZS1KeUxYbzBIZzExY3FFNzc0T2xRVjZZIiwieSI6Imt5dXViNDBXaTFoMENMVWFQZ3h5QXNFd1hpWlFLX2RXUXVzYjhZTllzMW8ifSwicmVjb3ZlcnlfY29tbWl0bWVudCI6IkVpQ3FBajBUanFoaU5LU1hWNllfTThzdFZFODNHVmltLXNwMHhzVERBTERaTlEifQ.eyJ1cGRhdGVfY29tbWl0bWVudCI6IkVpQlZMOEVDZ2ViaTUwSVE3SzVETzRxOUIxaGd0akxGWjhnWEJ0RWpiVjFxV0EiLCJwYXRjaGVzIjpbeyJhY3Rpb24iOiJyZXBsYWNlIiwiZG9jdW1lbnQiOnsicHVibGljS2V5cyI6W3siaWQiOiJzaWduaW5nS2V5IiwidHlwZSI6IlNlY3AyNTZrMVZlcmlmaWNhdGlvbktleTIwMTkiLCJqd2siOnsia3R5IjoiRUMiLCJjcnYiOiJzZWNwMjU2azEiLCJ4IjoiY0Y0ZFFOXzUxZGxOOXkzdml1REJWM2hOcm9zbHpha2dKYW9OZzJTcFNVYyIsInkiOiJVdWdtcVljZHJyWlBZdVZ1d0J6dENHRUtEYlZhbzE0OFd4ZjM5U0pOVTRRIn0sInVzYWdlIjpbIm9wcyIsImF1dGgiLCJnZW5lcmFsIl19XSwic2VydmljZUVuZHBvaW50cyI6W3siaWQiOiJzZXJ2aWNlRW5kcG9pbnRJZDEyMyIsInR5cGUiOiJzb21lVHlwZSIsInNlcnZpY2VFbmRwb2ludCI6Imh0dHBzOi8vd3d3LnVybC5jb20ifV19fV19
```
## Driver configuration via config.json
```
{
  "DriverConfiguration": {
    "Resilience": {
      "EnableRetry": true, // Enables/disables the retry policy for transient HTTP errors including 429. Default is false.
      "EnableCircuitBreaking": true // Enables/disables the circuit breaking policy for transient HTTP errors. Default is false.
    },
    "Consensus": {
      "Model": "FirstWins", // [FirstWins, PartialAgreement, FullAgreement] specifies the consensus model when multiple nodes are provided. Not currently used.
      "InAgreement": "1" // Specifies the number of nodes when PartialAgreement is specified. Not currently used.
    },
    "Nodes": [
      {
        "Name": "default", // [Required] Name of the node - currently must be set to 'default'.
        "Uri": "https://beta.discover.did.microsoft.com/1.0/identifiers/", // [Required] Uri to the node with trailing slash.
        "TimeoutInMilliseconds": "5000",
        "Use": "Always", // [Always, Random] specifies whether requests should always be sent to the node or randomly, in a multi-node setup. Not currently used.
      }
    ]
  }
}
```

## Backlog
* Add support for HTTPS
* Add 'multi-request' to ION network