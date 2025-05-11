# Views of Data Models

## Common Components and ViewModels

### Generic List Page Component
- EntityListPage<TBlock, TForm, TFilter>
  - Search bar (with optional filters)
  - List<TBlock> (block components)
  - Add button (opens TForm)
  - Edit/Delete on each block
  - Pagination, sorting, bulk actions
  - Loading/Error/Success/SelectedItem states
  - Detail modal/view for selected item (EntityDetail<T>)
  - Batch/bulk actions (delete, update) with List<int> SelectedIds
  - UI customization hooks: render props/slots/template parameters for custom actions/fields

### Generic Block Component
- EntityBlock<T>
  - Displays main fields of T
  - Edit/Delete buttons (generic ICommand/ActionCommand with CanExecute/async support)
  - Optional: Related info, custom alerts
  - UI customization hooks for extra actions/fields

### Generic Form Component
- EntityForm<T>
  - Input fields for T's properties
  - Save/Cancel buttons (generic ICommand/ActionCommand)
  - Validation and error display (Dictionary<string, string> ValidationErrors)
  - Validation logic in base class
  - UI customization hooks for custom fields/layout

### Generic Detail/Modal Component
- EntityDetail<T>
  - Displays all fields of T, related entities, and actions
  - UI customization hooks for related data, custom actions

## Explicit Filter Types
- FoodFilter: { string Name, int? CategoryId }
- ProductFilter: { string Name, int? IngredientId }
- RecipeFilter: { string Name, int? FoodId, int? RatingValue }
- PantryItemFilter: { string ProductName, DateTime? BestBefore }
- IngredientFilter: { string Name }
- ToolFilter: { string Name }
- RatingFilter: { string Title, int? RatingValue }
- RestaurantFilter: { string Name }
- OrderFilter: { int? RecipeId, int? RestaurantId, decimal? PriceMin, decimal? PriceMax }
- StepFilter: { string Title, int? ToolId, int? IngredientId }
- StepToolFilter: { int? StepId, int? ToolId }
- StepIngredientFilter: { int? StepId, int? IngredientId }
- SubRecipeFilter: { int? BaseRecipeId, int? SubRecipeId }
- RecipeHistoryFilter: { int? RecipeId, int? RatingId }

## API/DTO Layer Separation
- Use DTOs for API communication (e.g., FoodDto, ProductDto, etc.)
- Map DTOs to ViewModels in the client layer
- Document mapping strategies (e.g., AutoMapper, manual mapping)

## Extensibility
- To add a new entity:
  1. Define the data model and DTO
  2. Create Filter type
  3. Specialize EntityListPage, EntityBlock, EntityForm, EntityDetail for the entity
  4. Create ViewModels inheriting from generic base ViewModels
  5. Add API endpoints and mapping logic
  6. Add to navigation/menu

## Views as Page Components
- Food List: EntityListPage<FoodBlock, FoodForm, FoodFilter>
- Pantry: EntityListPage<PantryItemBlock, PantryItemForm, PantryItemFilter>
- Ingredients: EntityListPage<IngredientBlock, IngredientForm, IngredientFilter>
- Products: EntityListPage<ProductBlock, ProductForm, ProductFilter>
- Tools: EntityListPage<ToolBlock, ToolForm, ToolFilter>
- Recipes: EntityListPage<RecipeBlock, RecipeForm, RecipeFilter>
- Ratings: EntityListPage<RatingBlock, RatingForm, RatingFilter>
- Restaurants: EntityListPage<RestaurantBlock, RestaurantForm, RestaurantFilter>
- Orders: EntityListPage<OrderBlock, OrderForm, OrderFilter>
- Steps: EntityListPage<StepBlock, StepForm, StepFilter>
- StepTools: EntityListPage<StepToolBlock, StepToolForm, StepToolFilter>
- StepIngredients: EntityListPage<StepIngredientBlock, StepIngredientForm, StepIngredientFilter>
- SubRecipes: EntityListPage<SubRecipeBlock, SubRecipeForm, SubRecipeFilter>
- RecipeHistories: EntityListPage<RecipeHistoryBlock, RecipeHistoryForm, RecipeHistoryFilter>
// ...other entities as needed

## Views as Block Components
- Food Block: EntityBlock<Food> (custom fields: Name, RecipesCount, image, etc.)
- Recipe Block: EntityBlock<Recipe> (custom fields: Name, FoodName, StepCount, etc.)
- PantryItem Block: EntityBlock<PantryItem> (custom fields: ProductName, BestBefore, etc.)
- Ingredient Block: EntityBlock<Ingredient> (custom fields: Name, UsageCount)
- Product Block: EntityBlock<Product> (custom fields: Name, IngredientName, etc.)
- Tool Block: EntityBlock<Tool> (custom fields: Name, UsageCount)
- Rating Block: EntityBlock<Rating> (custom fields: Title, Detail, RatingValue, Date)
- Restaurant Block: EntityBlock<Restaurant> (custom fields: Name, OrderCount)
- Order Block: EntityBlock<Order> (custom fields: RecipeName, RestaurantName, Price, RatingTitle)
- Step Block: EntityBlock<Step> (custom fields: Title, Detail, Note, ToolCount, IngredientCount)
- StepTool Block: EntityBlock<StepTool> (custom fields: ToolName)
- StepIngredient Block: EntityBlock<StepIngredient> (custom fields: IngredientName, Amount, AmountType)
- SubRecipe Block: EntityBlock<SubRecipe> (custom fields: BaseRecipeName, SubRecipeName)
- RecipeHistory Block: EntityBlock<RecipeHistory> (custom fields: RecipeName, RatingTitle)
// ...other entities as needed

## Views as Form Components
- Food Form: EntityForm<Food>
- Recipe Form: EntityForm<Recipe>
- PantryItem Form: EntityForm<PantryItem>
- Ingredient Form: EntityForm<Ingredient>
- Product Form: EntityForm<Product>
- Tool Form: EntityForm<Tool>
- Rating Form: EntityForm<Rating>
- Restaurant Form: EntityForm<Restaurant>
- Order Form: EntityForm<Order>
- Step Form: EntityForm<Step>
- StepTool Form: EntityForm<StepTool>
- StepIngredient Form: EntityForm<StepIngredient>
- SubRecipe Form: EntityForm<SubRecipe>
- RecipeHistory Form: EntityForm<RecipeHistory>
// ...other entities as needed

## Views as Detail/Modal Components
- Food Detail: EntityDetail<Food>
- Recipe Detail: EntityDetail<Recipe>
- PantryItem Detail: EntityDetail<PantryItem>
- Ingredient Detail: EntityDetail<Ingredient>
- Product Detail: EntityDetail<Product>
- Tool Detail: EntityDetail<Tool>
- Rating Detail: EntityDetail<Rating>
- Restaurant Detail: EntityDetail<Restaurant>
- Order Detail: EntityDetail<Order>
- Step Detail: EntityDetail<Step>
- StepTool Detail: EntityDetail<StepTool>
- StepIngredient Detail: EntityDetail<StepIngredient>
- SubRecipe Detail: EntityDetail<SubRecipe>
- RecipeHistory Detail: EntityDetail<RecipeHistory>
// ...other entities as needed

# ViewModels of Views

## Generic List ViewModel
- EntityListViewModel<TBlockViewModel, TFilter>
  - string SearchText
  - TFilter Filter
  - List<TBlockViewModel> Items
  - ICommand AddItem
  - ICommand BulkDelete
  - List<int> SelectedIds
  - bool IsLoading
  - string ErrorMessage
  - string SuccessMessage
  - object SelectedItem

## Generic Block ViewModel
- EntityBlockViewModel
  - int Id
  - ICommand Edit
  - ICommand Delete

## Generic Form ViewModel
- EntityFormViewModel<T>
  - T Entity
  - ICommand Save
  - ICommand Cancel
  - Dictionary<string, string> ValidationErrors
  - Validation logic

## Generic Detail ViewModel
- EntityDetailViewModel<T>
  - T Entity
  - Related entities (as needed)
  - ICommand Edit
  - ICommand Delete
  - UI customization hooks

## Specializations (Examples)
- FoodListViewModel : EntityListViewModel<FoodBlockViewModel, FoodFilter>
- FoodBlockViewModel : EntityBlockViewModel + string Name, int RecipesCount, string image
- FoodFormViewModel : EntityFormViewModel<Food>
- FoodDetailViewModel : EntityDetailViewModel<Food>

- ProductListViewModel : EntityListViewModel<ProductBlockViewModel, ProductFilter>
- ProductBlockViewModel : EntityBlockViewModel + string Name, string IngredientName, double Amount, string AmountType, decimal Price
- ProductFormViewModel : EntityFormViewModel<Product>
- ProductDetailViewModel : EntityDetailViewModel<Product>

- RecipeListViewModel : EntityListViewModel<RecipeBlockViewModel, RecipeFilter>
- RecipeBlockViewModel : EntityBlockViewModel + string Name, string FoodName, string Detail, int StepCount, int RatingCount
- RecipeFormViewModel : EntityFormViewModel<Recipe>
- RecipeDetailViewModel : EntityDetailViewModel<Recipe>

- PantryListViewModel : EntityListViewModel<PantryItemBlockViewModel, PantryItemFilter>
- PantryItemBlockViewModel : EntityBlockViewModel + string ProductName, DateTime BestBefore, double RemainingAmount, string AmountType
- PantryItemFormViewModel : EntityFormViewModel<PantryItem>
- PantryItemDetailViewModel : EntityDetailViewModel<PantryItem>

- IngredientListViewModel : EntityListViewModel<IngredientBlockViewModel, IngredientFilter>
- IngredientBlockViewModel : EntityBlockViewModel + string Name, int UsageCount
- IngredientFormViewModel : EntityFormViewModel<Ingredient>
- IngredientDetailViewModel : EntityDetailViewModel<Ingredient>

- ToolListViewModel : EntityListViewModel<ToolBlockViewModel, ToolFilter>
- ToolBlockViewModel : EntityBlockViewModel + string Name, int UsageCount
- ToolFormViewModel : EntityFormViewModel<Tool>
- ToolDetailViewModel : EntityDetailViewModel<Tool>

- RatingListViewModel : EntityListViewModel<RatingBlockViewModel, RatingFilter>
- RatingBlockViewModel : EntityBlockViewModel + string Title, string Detail, int RatingValue, DateTime Date
- RatingFormViewModel : EntityFormViewModel<Rating>
- RatingDetailViewModel : EntityDetailViewModel<Rating>

- RestaurantListViewModel : EntityListViewModel<RestaurantBlockViewModel, RestaurantFilter>
- RestaurantBlockViewModel : EntityBlockViewModel + string Name, int OrderCount
- RestaurantFormViewModel : EntityFormViewModel<Restaurant>
- RestaurantDetailViewModel : EntityDetailViewModel<Restaurant>

- OrderListViewModel : EntityListViewModel<OrderBlockViewModel, OrderFilter>
- OrderBlockViewModel : EntityBlockViewModel + string RecipeName, string RestaurantName, decimal Price, string RatingTitle
- OrderFormViewModel : EntityFormViewModel<Order>
- OrderDetailViewModel : EntityDetailViewModel<Order>

- StepListViewModel : EntityListViewModel<StepBlockViewModel, StepFilter>
- StepBlockViewModel : EntityBlockViewModel + string Title, string Detail, string Note, int ToolCount, int IngredientCount
- StepFormViewModel : EntityFormViewModel<Step>
- StepDetailViewModel : EntityDetailViewModel<Step>

- StepToolListViewModel : EntityListViewModel<StepToolBlockViewModel, StepToolFilter>
- StepToolBlockViewModel : EntityBlockViewModel + string ToolName
- StepToolFormViewModel : EntityFormViewModel<StepTool>
- StepToolDetailViewModel : EntityDetailViewModel<StepTool>

- StepIngredientListViewModel : EntityListViewModel<StepIngredientBlockViewModel, StepIngredientFilter>
- StepIngredientBlockViewModel : EntityBlockViewModel + string IngredientName, double Amount, string AmountType
- StepIngredientFormViewModel : EntityFormViewModel<StepIngredient>
- StepIngredientDetailViewModel : EntityDetailViewModel<StepIngredient>

- SubRecipeListViewModel : EntityListViewModel<SubRecipeBlockViewModel, SubRecipeFilter>
- SubRecipeBlockViewModel : EntityBlockViewModel + string BaseRecipeName, string SubRecipeName
- SubRecipeFormViewModel : EntityFormViewModel<SubRecipe>
- SubRecipeDetailViewModel : EntityDetailViewModel<SubRecipe>

- RecipeHistoryListViewModel : EntityListViewModel<RecipeHistoryBlockViewModel, RecipeHistoryFilter>
- RecipeHistoryBlockViewModel : EntityBlockViewModel + string RecipeName, string RatingTitle
- RecipeHistoryFormViewModel : EntityFormViewModel<RecipeHistory>
- RecipeHistoryDetailViewModel : EntityDetailViewModel<RecipeHistory>

// Specializations for other entities can be added similarly
// All entities from "Explicit Filter Types" have now been specialized above.

# Entity Relationships
- Use generic sub-list/related-entity components: EntityRelatedList<TParent, TChild>
- Example: RecipeDetailViewModel includes EntityRelatedList<Recipe, Step>, EntityRelatedList<Recipe, Rating>
- Document relationship mapping in ViewModels and DTOs

# API/DTO Layer for ViewModels
- All ViewModels map to DTOs for API communication
- Use mapping utilities (e.g., AutoMapper) or manual mapping
- Keep ViewModel logic/UI state separate from DTOs/entities

# Extensibility for ViewModels
- To add a new entity, follow the documented steps above
- Use generic components and ViewModels for rapid development
- Add customizations via UI hooks, specialized fields, and relationship components