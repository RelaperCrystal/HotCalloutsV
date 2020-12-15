# Null Reference Issue

The callout `DocumentLack` can be spawned, but it cannot be accepted. It will somehow throw the `NullReferenceException`, and it cannot be caught.

## Where Is The Exception Thrown

From stack-traces, I managed to resolve some of mystery.

### Am I wrong?
Seems like my debugging was somewhat inperfect - I didn't copy over Program Database (symbols) file along with the `dll` file. I will test that later.

### Thrown by LSPDFR instead of The Callout

The try-catch in the code is not effective and cannot print required information on where it's located, and logs in `OnCalloutAccepted` also not appearing in the log file and console. This suggests this exception may have thrown on LSPDFR itself. It's possible that instance of this callout suddenly becomes `null` when starting callout. Unfortunately, LSPDFR is closed-source and cannot be analyzed. If you suspect this issued is from LSPDFR, report to G17 Media and at me `@RelaperCrystal` in the post.

### Is Stop The Ped issue?

The only susceptible code in `OnCalloutAccepted` is to add information of Uninsured and Unregistered information to vehicle by using Stop The Ped API. If you suspect this issued is from Stop the Ped, contact `Bejoijo`. If you decided to post something for this, `@RelaperCrystal` in the post.

### No End Messages

I have added End Message to the callout but it does not appear in the screen, log file, console, etc.

## End

If you think something wrong with the implementation of the integrate, please send me an issue or pull request with modifications. Leave comments in code.
