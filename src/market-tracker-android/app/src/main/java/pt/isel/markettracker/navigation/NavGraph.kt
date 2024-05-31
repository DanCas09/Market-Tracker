package pt.isel.markettracker.ui.screens

import androidx.compose.animation.EnterTransition
import androidx.compose.animation.ExitTransition
import androidx.compose.foundation.gestures.detectHorizontalDragGestures
import androidx.compose.foundation.layout.padding
import androidx.compose.material3.Scaffold
import androidx.compose.runtime.Composable
import androidx.compose.runtime.collectAsState
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableIntStateOf
import androidx.compose.runtime.saveable.rememberSaveable
import androidx.compose.runtime.setValue
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.input.pointer.pointerInput
import androidx.hilt.navigation.compose.hiltViewModel
import androidx.navigation.compose.NavHost
import androidx.navigation.compose.composable
import androidx.navigation.compose.rememberNavController
import pt.isel.markettracker.navigation.Destination
import pt.isel.markettracker.navigation.NavBar
import pt.isel.markettracker.navigation.toDestination
import pt.isel.markettracker.repository.auth.AuthEvent
import pt.isel.markettracker.repository.auth.IAuthRepository
import pt.isel.markettracker.ui.screens.list.listOfProducts.ListScreen
import pt.isel.markettracker.ui.screens.list.listOfProducts.ListScreenViewModel
import pt.isel.markettracker.ui.screens.login.LoginScreen
import pt.isel.markettracker.ui.screens.login.LoginScreenViewModel
import pt.isel.markettracker.ui.screens.products.ProductsScreen
import pt.isel.markettracker.ui.screens.products.ProductsScreenViewModel
import pt.isel.markettracker.ui.screens.profile.ProfileScreen
import pt.isel.markettracker.ui.screens.profile.ProfileScreenViewModel

@Composable
fun NavGraph(
    onProductClick: (String) -> Unit,
    onListClick: (Int) -> Unit,
    onBarcodeScanRequest: () -> Unit,
    onSignUpRequested: () -> Unit,
    authRepository: IAuthRepository,
    productsScreenViewModel: ProductsScreenViewModel = hiltViewModel(),
    listScreenViewModel: ListScreenViewModel = hiltViewModel(),
    loginScreenViewModel: LoginScreenViewModel = hiltViewModel(),
    profileScreenViewModel: ProfileScreenViewModel = hiltViewModel()
) {
    val navController = rememberNavController()
    var selectedIndex by rememberSaveable { mutableIntStateOf(0) }

    val authState by authRepository.authState.collectAsState()

    navController.addOnDestinationChangedListener { _, destination, _ ->
        selectedIndex = destination.route?.toDestination()?.ordinal ?: 0
    }

    fun changeDestination(destination: String) {
        navController.navigate(destination) {
            popUpTo(navController.graph.startDestinationId) {
                saveState = true
            }
            launchSingleTop = true
            restoreState = true
        }
    }

    Scaffold(
        modifier = Modifier.pointerInput(selectedIndex) {
            detectHorizontalDragGestures { change, dragAmount ->
                change.consume()
                // choosing direction I want to slide
                selectedIndex = if (dragAmount < 0) selectedIndex.inc() else selectedIndex.dec()

                // making sure It doesn't go out of borders
                selectedIndex =
                    if (selectedIndex in 0 until Destination.entries.size) selectedIndex
                    else if (selectedIndex < 0) 0
                    else Destination.entries.indices.last

                val newDestination = Destination.entries[selectedIndex].route
                changeDestination(newDestination)
            }
        },
        contentColor = Color.Black,
        bottomBar = {
            NavBar(
                Destination.entries,
                selectedIndex = selectedIndex,
                onItemClick = { route ->
                    changeDestination(route)
                }
            )
        }
    ) { paddingValues ->
        NavHost(
            navController = navController,
            startDestination = Destination.HOME.route,
            modifier = Modifier.padding(paddingValues),
            enterTransition = { EnterTransition.None },
            exitTransition = { ExitTransition.None }
        ) {
            composable(Destination.HOME.route) {
                ProductsScreen(onProductClick, onBarcodeScanRequest)
            }

            composable(Destination.LIST.route) {
                ListScreen(onListClick, listScreenViewModel)
            }

            composable(Destination.PROFILE.route) {
                when (authState) {
                    is AuthEvent.Login -> {
                        ProfileScreen(profileScreenViewModel)
                    }

                    else -> {
                        LoginScreen(
                            onSignUpRequested = onSignUpRequested
                        )
                    }
                }
            }
        }
    }
}