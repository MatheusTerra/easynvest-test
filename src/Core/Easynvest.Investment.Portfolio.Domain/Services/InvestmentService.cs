using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Easynvest.Investment.Portfolio.Domain.Configs;
using Easynvest.Investment.Portfolio.Domain.Entities;
using Easynvest.Investment.Portfolio.Domain.Entities.Abstracts;
using Easynvest.Investment.Portfolio.Domain.Notifications;
using Easynvest.Investment.Portfolio.Domain.Queries.Repositories;
using Easynvest.Investment.Portfolio.Domain.Services.Abstract;

namespace Easynvest.Investment.Portfolio.Domain.Services
{
    public class InvestmentService : IInvestmentService
    {
        private readonly NotificationPool _notificationPool;
        private readonly ITesouroDiretoQueryRepository _tesouroDiretoQueryRepository;
        private readonly IRendaFixaQueryRepository _rendaFixaQueryRepository;
        private readonly IFundosQueryRepository _fundosQueryRepository;
        private readonly InvestmentsIRFeeConfig _investmentsIRFeeConfig;

        public InvestmentService(
            NotificationPool notificationPool,
            ITesouroDiretoQueryRepository tesouroDiretoQueryRepository,
            IRendaFixaQueryRepository rendaFixaQueryRepository,
            IFundosQueryRepository fundosQueryRepository,
            InvestmentsIRFeeConfig investmentsIRFeeConfig)
        {
            _notificationPool = notificationPool;
            _tesouroDiretoQueryRepository = tesouroDiretoQueryRepository;
            _rendaFixaQueryRepository = rendaFixaQueryRepository;
            _fundosQueryRepository = fundosQueryRepository;
            _investmentsIRFeeConfig = investmentsIRFeeConfig;
        }

        public bool HasNotifications => _notificationPool.HasNotifications;
        public IReadOnlyCollection<Notification> Notifications => _notificationPool.Notifications;

        public async Task<IEnumerable<Investimento>> GetInvestments()
        {
            var investments = new ConcurrentBag<Investimento>();
            var tasks = new List<Task>();

            tasks.Add(TesouroDiretoTask(investments));
            tasks.Add(RendaFixaTask(investments));
            tasks.Add(FundosTask(investments));

            await Task.WhenAll(tasks);

            return investments;
        }

        private async Task TesouroDiretoTask(ConcurrentBag<Investimento> investments)
        {
            var tesouroDiretoList = await _tesouroDiretoQueryRepository.Get();

            if (_tesouroDiretoQueryRepository.HasNotifications)
                return;

            CreateInvestments(investments, tesouroDiretoList, _investmentsIRFeeConfig.TesouroDireto);
        }

        private async Task RendaFixaTask(ConcurrentBag<Investimento> investments)
        {
            var rendaFixaList = await _rendaFixaQueryRepository.Get();

            if (_rendaFixaQueryRepository.HasNotifications)
                return;

            CreateInvestments(investments, rendaFixaList, _investmentsIRFeeConfig.RendaFixa);
        }

        private async Task FundosTask(ConcurrentBag<Investimento> investments)
        {
            var fundosList = await _fundosQueryRepository.Get();

            if (_fundosQueryRepository.HasNotifications)
                return;

            CreateInvestments(investments, fundosList, _investmentsIRFeeConfig.Fundos);
        }

        private void CreateInvestments(ConcurrentBag<Investimento> investimentos, IEnumerable<InvestimentoBase> subject, decimal taxaIR)
        {
            foreach (var item in subject)
                investimentos.Add(item.CreateInvestimento(taxaIR));
        }
    }
}