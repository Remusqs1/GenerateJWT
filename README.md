# GenerateJWT

This was a coding challenge I solved during the selection process for Acklen Avenue. The requirement was as follows:

Write a simple class called MainClass with a method string GenerateCustomJwt(params) that takes a secret, issuer, audience, subject, JWT ID, and issued at time as parameters and generates a JWT with the following claims:

sub (subject) with the provided value
jti (JWT ID) with the provided value
iat (issued at) with the provided Unix timestamp value
The JWT should not have an expiration claim and should be signed using the HMAC SHA-256 algorithm.
