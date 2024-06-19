package pt.isel.markettracker.http.service.operations.auth

import android.util.Log
import com.google.gson.Gson
import okhttp3.OkHttpClient
import pt.isel.markettracker.http.models.token.GoogleTokenCreationInputModel
import pt.isel.markettracker.http.models.token.TokenCreationInputModel
import pt.isel.markettracker.http.service.MarketTrackerService

private const val authPath = "/auth"
private const val googleSignInPath = "${authPath}/google-sign-in"
private const val marketTrackerSignInPath = "${authPath}/sign-in"
private const val marketTrackerSignOutPath = "${authPath}/sign-out"

class AuthService(
    override val httpClient: OkHttpClient,
    override val gson: Gson
) : IAuthService, MarketTrackerService() {

    override suspend fun googleSignIn(input: GoogleTokenCreationInputModel) {
        return requestHandler(
            path = googleSignInPath,
            method = HttpMethod.POST,
            input = input
        )
    }

    override suspend fun signIn(input: TokenCreationInputModel) {
        Log.v("TokenService", "createToken")
        requestHandler<Unit>(
            path = marketTrackerSignInPath,
            method = HttpMethod.POST,
            input = input
        )
        //FirebaseMessaging.getInstance().token.addOnCompleteListener {
        //    if (!it.isSuccessful) {
        //        return@addOnCompleteListener
        //    }
        //    val token = it.result
        //    Log.v("TokenService", token) // TODO: upload token to server
        //}
    }

    override suspend fun signOut() {
        return requestHandler(
            path = marketTrackerSignOutPath,
            method = HttpMethod.DELETE
        )
    }
}