Issue to discuss: OSRM requires that you separate the "from" coordinates with the "to" coordinates with a semicolon ";". Unfortunately that causes the uri to break, as everything after the semicolon gets cut off.
If I encode the semicolon as %3B OSRM doesn't seem to recognize the request. I didn't manage to solve this, and didn't have time to change to GraphHopper or other ETA/Route services :(
