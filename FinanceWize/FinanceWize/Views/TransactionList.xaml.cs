using CommunityToolkit.Mvvm.Messaging;
using FinanceWize.Models;
using FinanceWize.Repositories;

namespace FinanceWize.Views;

public partial class TransactionList : ContentPage
{
	private ITransactionRepository _repository;
	public TransactionList(ITransactionRepository repository)
	{

		_repository = repository;
		InitializeComponent();

		Reloade();

        WeakReferenceMessenger.Default.Register<string>(this, (e, msg) =>
		{
			Reloade();
        });

	}

	private void Reloade()
	{
		var items = _repository.GetAll();
        CollectionViewTransactions.ItemsSource = items;

		double income = items.Where(x => x.Type == Enum.TransactionType.Income).Sum(x=> x.Value);
        double expense = items.Where(x => x.Type == Enum.TransactionType.Expense).Sum(x => x.Value);
		double balance = income- expense;

		LabelIncome.Text = income.ToString("C");
		LabelExpense.Text = expense.ToString("C");
		LabelBalance.Text = balance.ToString("C");
    }

	private void OnButtonClicked_To_TransactionAdd(object sender, EventArgs args)
	{
		
		var transactionAdd = Handler.MauiContext.Services.GetService<TransactionAdd>();

		Navigation.PushModalAsync(transactionAdd);
		
	}

   

    private void TapGestureRecognizer_Tapped_To_TransactionEdit(object sender, TappedEventArgs e)
    {
		var grid = (Grid)sender;
		var gesture = (TapGestureRecognizer)grid.GestureRecognizers[0];
		Transaction trasaction = (Transaction)gesture.CommandParameter;

        var transactionEdit = Handler.MauiContext.Services.GetService<TransactionEdit>();
		transactionEdit.SetTransactionToEdit(trasaction);
        Navigation.PushModalAsync(transactionEdit);

    }

    private async void TapGestureRecognizer_Tapped_ToDelete(object sender, TappedEventArgs e)
    {
		await AnimationBorder((Border)sender, true);
		bool result = await App.Current.MainPage.DisplayAlert("Delete!", "Delete this item?", "Yes", "No");

		if (result)
		{
            Transaction trasaction = (Transaction)e.Parameter;
			_repository.Delete(trasaction);

			Reloade();
		}
		else
		{
			await AnimationBorder((Border)sender, false);
		}
    }

	private Color _borderOriginalBackgroundColor;
	private String _labelOriginalText;

	private async Task AnimationBorder(Border border, bool IsDeleteAnimation)
	{
		var label = (Label)border.Content;

		if (IsDeleteAnimation)
		{
			_borderOriginalBackgroundColor = border.BackgroundColor;
			_labelOriginalText = label.Text;

			await border.RotateYTo(90, 0400);

			border.BackgroundColor = Colors.Red;

			label.TextColor= Colors.White;
			label.Text= "X";

            await border.RotateYTo(180, 0400);
        }
		else
		{
			await border.RotateYTo(90, 0400);

			border.BackgroundColor = _borderOriginalBackgroundColor;
			label.TextColor= Colors.Black;
			label.Text= _labelOriginalText;

            await border.RotateYTo(0, 0400);
        }
	}
}