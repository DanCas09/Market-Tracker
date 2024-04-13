package pt.isel.markettracker.ui.theme

import androidx.compose.foundation.isSystemInDarkTheme
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.darkColorScheme
import androidx.compose.material3.lightColorScheme
import androidx.compose.runtime.Composable
import androidx.compose.ui.graphics.Color

private val LightColorScheme = lightColorScheme(
    primary = Primary,
    background = Grey,
    onBackground = Color.Black,
    surface = Grey,
    onSurface = Color.Black
    /*
    onPrimary = Color.Black,
    primaryContainer = Color.Black,
    onPrimaryContainer = Color.Black,
    inversePrimary = Color.Black,
    secondary = Color.Black,
    onSecondary = Color.Black,
    secondaryContainer = Color.Black,
    onSecondaryContainer = Color.Black,
    tertiary = Color.Black,
    onTertiary = Color.Black,
    tertiaryContainer = Color.Black,
    onTertiaryContainer = Color.Black,
    background = Color.Black,
    onBackground = Color.Black,
    surface = Color.Black,
    onSurface = Color.Black,
    surfaceVariant = Color.Black,
    onSurfaceVariant = Color.Black,
    surfaceTint = Color.Black,
    inverseSurface = Color.Black,
    inverseOnSurface = Color.Black,
    error = Color.Black,
    onError = Color.Black,
    errorContainer = Color.Black,
    onErrorContainer = Color.Black,
    outline = Color.Black,
    outlineVariant = Color.Black,
    scrim = Color.Black,
    surfaceBright = Color.Black,
    surfaceContainer = Color.Black,
    surfaceContainerHigh = Color.Black,
    surfaceContainerHighest = Color.Black,
    surfaceContainerLow = Color.Black,
    surfaceContainerLowest = Color.Black,
    surfaceDim = Color.Black*/
)

private val DarkColorScheme = darkColorScheme(
    primary = Primary,
    background = Grey,
    onBackground = Color.Black,
    onSurface = Color.Black
)

@Composable
fun MarkettrackerTheme(
    isDarkTheme: Boolean = isSystemInDarkTheme(),
    content: @Composable () -> Unit
) {
    MaterialTheme(
        colorScheme = if (isDarkTheme) DarkColorScheme else LightColorScheme,
        typography = MarketTrackerTypography,
        content = content
    )
}