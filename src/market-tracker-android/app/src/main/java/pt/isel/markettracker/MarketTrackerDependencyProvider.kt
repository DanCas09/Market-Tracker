package pt.isel.markettracker

import com.google.gson.Gson
import okhttp3.OkHttpClient
import pt.isel.markettracker.http.service.operations.user.IUserService

interface MarketTrackerDependencyProvider {
    /**
     * The HTTP client used to perform HTTP requests
     */
    val httpClient: OkHttpClient

    /**
     * The JSON serializer/deserializer used to convert JSON into DTOs
     */
    val gson: Gson

    /**
     * The service used to fetch users
     */
    val userService: IUserService

}